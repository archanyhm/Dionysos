using Microsoft.EntityFrameworkCore;

namespace Dionysos.Database;

public class MainDbContext : DbContext, IMainDbContext
{
    public DbSet<Article> Articles { get; set; }
    public DbSet<InventoryItem> InventoryItems { get; set; }
    
    public DbSet<Vendor> Vendors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseLazyLoadingProxies()
            .UseNpgsql(
                @"Host=localhost;Username=postgres;Password=postgres;Database=dionysos;Port=5433");
    }
}