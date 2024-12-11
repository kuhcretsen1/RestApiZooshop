using ZooShop.Infrastructure.Persistence.Repositories;
using ZooShop.Application.Common.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using ZooShop.Domain.Categorys;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using ZooShop.Application.Common.Interfaces.Queries;

namespace ZooShop.Infrastructure;

public static class ConfigurePersistence
{
    public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var dataSourceBuilder = new NpgsqlDataSourceBuilder("Host=localhost;Database=ZooShopRestDb;Username=postgres;Password=postgres");
        dataSourceBuilder.EnableDynamicJson();
        var dataSource = dataSourceBuilder.Build();

        services.AddDbContext<ZooShopDbContext>(options =>
            options.UseNpgsql(
                    dataSource,
                    builder => builder.MigrationsAssembly(typeof(ZooShopDbContext).Assembly.FullName))
                .UseSnakeCaseNamingConvention()
                .ConfigureWarnings(w => w.Ignore(CoreEventId.ManyServiceProvidersCreatedWarning)));
        services.AddTransient<ZooShopDbContextInitialiser>();
        services.AddRepositories();
    }



    private static void AddRepositories(this IServiceCollection services)
    {
        
        services.AddScoped<OrderRepository>();
        services.AddScoped<IOrderRepository>(provider => provider.GetRequiredService<OrderRepository>());
        services.AddScoped<IOrderQueries>(provider => provider.GetRequiredService<OrderRepository>());

        services.AddScoped<CategoryRepository>();
        services.AddScoped<ICategoryRepository>(provider => provider.GetRequiredService<CategoryRepository>());
        services.AddScoped<ICategoryQueries>(provider => provider.GetRequiredService<CategoryRepository>());
        
        services.AddScoped<AnimalRepository>();
        services.AddScoped<IAnimalRepository>(provider => provider.GetRequiredService<AnimalRepository>());
        services.AddScoped<IAnimalQueries>(provider => provider.GetRequiredService<AnimalRepository>());
        
        services.AddScoped<CustomerRepository>();
        services.AddScoped<ICustomerRepository>(provider => provider.GetRequiredService<CustomerRepository>());
        services.AddScoped<ICustomerQueries>(provider => provider.GetRequiredService<CustomerRepository>());
        
        services.AddScoped<ProductRepository>();
        services.AddScoped<IProductRepository>(provider => provider.GetRequiredService<ProductRepository>());
        services.AddScoped<IProductQueries>(provider => provider.GetRequiredService<ProductRepository>());
    }

}