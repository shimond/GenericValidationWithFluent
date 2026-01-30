using FluentValidation;
using GenericValidationWithFluent.ExceptionHandlers;
using GenericValidationWithFluent.Routes;
using GenericValidationWithFluent.Services.Notifications;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// Register AutoMapper with all profiles in the assembly
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Register keyed notification services - Same interface, different implementations
builder.Services.AddKeyedScoped<INotificationService, EmailNotificationService>("email");
builder.Services.AddKeyedScoped<INotificationService, SmsNotificationService>("sms");
builder.Services.AddKeyedScoped<INotificationService, PushNotificationService>("push");

// Register the notification manager
builder.Services.AddScoped<NotificationManager>();

// Add exception handlers in order: Most Specific -> General
// 1. Handle FluentValidation exceptions
builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
// 2. Handle custom application exceptions
builder.Services.AddExceptionHandler<ApplicationExceptionHandler>();
// 3. Handle all other unhandled exceptions
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddProblemDetails();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapProductsRoutes();

app.Run();
