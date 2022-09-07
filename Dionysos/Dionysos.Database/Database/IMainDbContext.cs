using Microsoft.EntityFrameworkCore;

namespace Dionysos.Dionysos.Database.Database;

public interface IMainDbContext
{
    DbSet<Article> Articles { get; set; }
    DbSet<InventoryItem> InventoryItems { get; set; }
    DbSet<Vendor> Vendors { get; set; }
    int SaveChanges();
}
