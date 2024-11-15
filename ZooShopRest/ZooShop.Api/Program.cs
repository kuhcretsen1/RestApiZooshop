using Microsoft.EntityFrameworkCore;
using ZooShop.Infrastructure;
using System.Reflection;
using MediatR;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Налаштуй DbContext
builder.Services.AddDbContext<ZooShopDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ZooShopDatabase")));
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
// Додай сервіси MediatR, CQRS, тощо

// Додай контролери
builder.Services.AddControllers();
var app = builder.Build();

// Інші налаштування
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();