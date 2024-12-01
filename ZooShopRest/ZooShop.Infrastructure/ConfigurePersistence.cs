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
        var dataSourceBuilder = new NpgsqlDataSourceBuilder(configuration.GetConnectionString("Default"));
        dataSourceBuilder.EnableDynamicJson();
        var dataSource = dataSourceBuilder.Build();

        services.AddDbContext<ZooShopDbContext>(options =>
            options.UseNpgsql(
                    dataSource,
                    builder => builder.MigrationsAssembly(typeof(ZooShopDbContext).Assembly.FullName))
                .UseSnakeCaseNamingConvention()
                .ConfigureWarnings(w => w.Ignore(CoreEventId.ManyServiceProvidersCreatedWarning)));

        services.AddRepositories();
    }



    private static void AddRepositories(this IServiceCollection services)
    {
        // Приклад додавання репозиторіїв
        services.AddScoped<IAnimalRepository, AnimalRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
    }

}