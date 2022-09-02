using Dionysos.Database;
using Dionysos.Dtos;
using Dionysos.Extensions;

namespace Dionysos.Services.InventoryItemServices;

public class InventoryItemFetchingService
{
    private readonly IMainDbContext _dbContext;

    public InventoryItemFetchingService(IMainDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<InventoryItemDto> FetchItems()
    {
        var items = _dbContext.InventoryItems.ToList();

        return items.Select(x => x.ToInventoryItemDto()).ToList();
    }

    public InventoryItemDto FetchItem(int id)
    {
        var item = _dbContext.InventoryItems
            .Where(x => x.Id == id)
            .Select(x => x.ToInventoryItemDto())
            .Single();
        return item;
    }
}