using Microsoft.EntityFrameworkCore;
using ZooShop.Infrastructure;
using System.Reflection;
using MediatR;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Налаштуй DbContext
builder.Services.AddDbContext<ZooShopDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ZooShopDatabase")));

// Додай MediatR
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// Додай FluentValidation
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddFluentValidationAutoValidation();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "ZooShop API",
        Version = "v1"
    });
});

// Додай контролери
builder.Services.AddControllers();

var app = builder.Build();

// Інші налаштування
app.UseSwagger(c =>
{
    c.RouteTemplate = "swagger/{documentName}/swagger.json";
});
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ZooShop API V1");
    c.RoutePrefix = string.Empty; // Робить Swagger кореневим маршрутом
});
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();