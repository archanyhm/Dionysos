using Microsoft.EntityFrameworkCore;

namespace Dionysos.Database;

public interface IMainDbContext
{
    DbSet<Article> Articles { get; set; }
    DbSet<InventoryItem> InventoryItems { get; set; }
    int SaveChanges();
}