using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Dionysos.Database;

public class MainDbContext : DbContext, IMainDbContext
{
    public DbSet<Article> Articles { get; set; }
    public DbSet<InventoryItem> InventoryItems { get; set; }

    public override int SaveChanges()
    {
        return base.SaveChanges();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseLazyLoadingProxies()
            .UseNpgsql(
                @"Host=localhost;Username=postgres;Password=postgres;Database=dionysos;Port=5433");
    }
}

public class Article
{
    [Key]
    public string Ean { get; set; }
    public string Name { get; set; }
    public string Vendor { get; set; }
    public string Description { get; set; }
    public virtual List<InventoryItem> InventoryItems { get; set; }
}
public class InventoryItem
{
    public int Id { get; set; }
    public DateTime? BestBefore { get; set; }
    
    [ForeignKey("Article")]
    public string Ean { get; set; }
    public virtual Article Article { get; set; }
}