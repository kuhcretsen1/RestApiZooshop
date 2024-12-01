using Microsoft.EntityFrameworkCore;

namespace ZooShop.Infrastructure;

public class ZooShopDbContextInitialiser
{
    private readonly ZooShopDbContext _context;

    public ZooShopDbContextInitialiser(ZooShopDbContext context)
    {
        _context = context;
    }

    public async Task InitializeAsync()
    {
        await _context.Database.MigrateAsync();
    }
}
