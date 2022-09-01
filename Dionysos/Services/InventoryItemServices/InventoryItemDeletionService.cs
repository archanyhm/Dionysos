using Dionysos.Database;

namespace Dionysos.Services.InventoryItemServices;

public class InventoryItemDeletingService
{
    private readonly IMainDbContext _dbContext;

    public InventoryItemDeletingService(IMainDbContext mainDbContext)
    {
        _dbContext = mainDbContext;
    }

    public void DeleteInventoryItem(int id)
    {
        var item = _dbContext.InventoryItems.SingleOrDefault(x => x.Id == id);
        if (item is not null)
        {
            _dbContext.InventoryItems.Remove(item);
        }
        _dbContext.SaveChanges();
    }
}