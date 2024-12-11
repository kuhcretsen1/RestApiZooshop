using ZooShop.Application;
using ZooShop.Api;
using ZooShop.Infrastructure;
using ZooShop.Application.Common.Interfaces;
using ZooShop.Application.Common.Interfaces.Repositories;
using ZooShop.Application.Common.Interfaces.Queries;
using ZooShop.Infrastructure.Persistence.Migrations;
using ZooShop.Application.Common.Interfaces.Repositories;
using ZooShop.Application.Common.Interfaces.Queries;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.SetupServices();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await app.InitialiseDb();
app.MapControllers();

app.Run();