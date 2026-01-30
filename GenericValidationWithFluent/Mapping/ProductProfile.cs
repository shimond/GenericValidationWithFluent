using AutoMapper;
using GenericValidationWithFluent.DTOs;
using GenericValidationWithFluent.Entities;

namespace GenericValidationWithFluent.Mapping;

/// <summary>
/// AutoMapper profile for Product-related mappings
/// Defines how to map between DTOs and Entities
/// </summary>
public class ProductProfile : Profile
{
    public ProductProfile()
    {
        // ============================================
        // DTO to Entity Mappings (for Create/Update)
        // ============================================
        
        // CreateProductDTO -> Product
        CreateMap<CreateProductDTO, Product>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) // ID is auto-generated
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true))
            .ForMember(dest => dest.CategoryId, opt => opt.Ignore())
            .ForMember(dest => dest.Category, opt => opt.Ignore());

        // UpdateProductDTO -> Product
        CreateMap<UpdateProductDTO, Product>()
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // Don't update CreatedAt
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.IsActive, opt => opt.Ignore()) // Don't change active status
            .ForMember(dest => dest.CategoryId, opt => opt.Ignore())
            .ForMember(dest => dest.Category, opt => opt.Ignore());

        // ============================================
        // Entity to DTO Mappings (for Responses)
        // ============================================
        
        // Product -> ProductResponseDTO (full details)
        CreateMap<Product, ProductResponseDTO>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));

        // Product -> ProductListItemDTO (lightweight for lists)
        CreateMap<Product, ProductListItemDTO>();

        // Category -> CategoryResponseDTO
        CreateMap<Category, CategoryResponseDTO>();

        // ============================================
        // Advanced Mapping Examples
        // ============================================
        
        // Example: Conditional mapping
        // CreateMap<Product, ProductResponseDTO>()
        //     .ForMember(dest => dest.Category, 
        //         opt => opt.MapFrom((src, dest, destMember, context) => 
        //             src.Category != null ? context.Mapper.Map<CategoryResponseDTO>(src.Category) : null));

        // Example: Custom value resolver
        // CreateMap<Product, ProductResponseDTO>()
        //     .ForMember(dest => dest.Name, 
        //         opt => opt.MapFrom(src => src.Name.ToUpper()));

        // Example: Flattening
        // CreateMap<Product, ProductWithCategoryNameDTO>()
        //     .ForMember(dest => dest.CategoryName, 
        //         opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : "N/A"));

        // Example: ReverseMap for bidirectional mapping
        // CreateMap<Product, ProductResponseDTO>().ReverseMap();
    }
}
