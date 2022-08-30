using Dionysos.Database;

namespace Dionysos.Services.InventoryItemServices;

public class InventoryItemDeletingService
{
    private readonly MainDbContext _dbContext;

    public InventoryItemDeletingService()
    {
        _dbContext = new MainDbContext();
    }

    public void DeleteInventoryItem(int id)
    {
        var item = _dbContext.InventoryItems.Single(x => x.Id == id);
        _dbContext.InventoryItems.Remove(item);
        _dbContext.SaveChanges();
    }
}