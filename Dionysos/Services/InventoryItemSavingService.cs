using Dionysos.Database;
using Dionysos.Dtos;

namespace Dionysos.Services;

public class InventoryItemSavingService
{
    private readonly MainDbContext _mainDbContext;

    public InventoryItemSavingService(MainDbContext mainDbContext)
    {
        _mainDbContext = mainDbContext;
    }
    public void SaveArticle(InventoryItemDto inventoryItemDto)
    {
        var isItemAlreadyKnown = _mainDbContext.InventoryItems.Any(x => x.Id == inventoryItemDto.Id);
        if (!isItemAlreadyKnown)
        {
            var newItem = new InventoryItem
            {
                BestBefore = inventoryItemDto.BestBefore,
                Ean = inventoryItemDto.Ean
            };
            _mainDbContext.InventoryItems.Add(newItem);
        }
        
        _mainDbContext.SaveChanges();
    }
}