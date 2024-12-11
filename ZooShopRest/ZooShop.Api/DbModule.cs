using ZooShop.Infrastructure;
using ZooShop.Infrastructure.Persistence;

namespace ZooShop.Api;

public static class DbModule
{
    public static async Task InitialiseDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var initialiser = scope.ServiceProvider.GetRequiredService<ZooShopDbContextInitialiser>();
        await initialiser.InitializeAsync();
    }
}