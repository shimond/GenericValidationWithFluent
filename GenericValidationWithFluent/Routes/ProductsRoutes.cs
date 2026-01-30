using AutoMapper;
using GenericValidationWithFluent.DTOs;
using GenericValidationWithFluent.Entities;
using GenericValidationWithFluent.Exceptions;
using GenericValidationWithFluent.Filters;
using GenericValidationWithFluent.Services.Notifications;

namespace GenericValidationWithFluent.Routes;

public static class ProductsRoutes
{
    public static void MapProductsRoutes(this WebApplication app)
    {
        var group = app.MapGroup("/products").AddEndpointFilter<ValidationhFilter>();

        group.MapPost("/", CreateProduct);
        group.MapPut("/", UpdateProduct);
        
        // New endpoint to test notifications with different channels
        group.MapPost("/{id:int}/notify", NotifyProductUpdate);
    }

    private static async Task<IResult> CreateProduct(
        CreateProductDTO dto,
        NotificationManager notificationManager)
    {
        // Send notification after product creation
        await notificationManager.SendAsync(
            channel: "email",
            recipient: "admin@example.com",
            subject: "New Product Created",
            message: $"Product '{dto.Name}' has been created with price ${dto.Price}");

        return Results.Ok(new { Message = "Product created successfully", Data = dto });
    }

    private static IResult UpdateProduct(UpdateProductDTO dto)
    {
        return Results.Ok(new { Message = "Product updated successfully", Data = dto });
    }

    private static async Task<IResult> NotifyProductUpdate(
        int id,
        string channel,
        NotificationManager notificationManager)
    {
        // Validate channel
        if (!notificationManager.GetAvailableChannels().Contains(channel))
        {
            return Results.BadRequest(new
            {
                Error = "Invalid notification channel",
                AvailableChannels = notificationManager.GetAvailableChannels()
            });
        }

        // Send notification through specified channel
        var success = await notificationManager.SendAsync(
            channel: channel,
            recipient: channel switch
            {
                "email" => "user@example.com",
                "sms" => "+1234567890",
                "push" => "device-token-12345",
                _ => "unknown"
            },
            subject: "Product Update",
            message: $"Product with ID {id} has been updated");

        return success
            ? Results.Ok(new { Message = $"Notification sent via {channel}", ProductId = id })
            : Results.Problem("Failed to send notification");
    }
}
