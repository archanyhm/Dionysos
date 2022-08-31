using Dionysos.Database;
using Dionysos.Dtos;

namespace Dionysos.Services.InventoryItemServices;

public class InventoryItemSavingService
{
    private readonly IMainDbContext _mainDbContext;

    public InventoryItemSavingService(IMainDbContext mainDbContext)
    {
        _mainDbContext = mainDbContext;
    }
    public void SaveInventoryItem(InventoryItemDto inventoryItemDto)
    {
        var isItemAlreadyKnown = _mainDbContext.InventoryItems.Any(x => x.Id == inventoryItemDto.Id);
        if (!isItemAlreadyKnown)
        {
            var newItem = CreateDbInventoryItem(inventoryItemDto);
            _mainDbContext.InventoryItems.Add(newItem);
        }
        
        _mainDbContext.SaveChanges();
    }

    private static InventoryItem CreateDbInventoryItem(InventoryItemDto inventoryItemDto)
    {
        return new InventoryItem
        {
            BestBefore = inventoryItemDto.BestBefore,
            Ean = inventoryItemDto.Ean
        };
    }

    public void UpdateInventoryItem(InventoryItemDto inventoryItemDto)
    {
        _mainDbContext.InventoryItems.Update(CreateDbInventoryItem(inventoryItemDto));
        _mainDbContext.SaveChanges();
    }
}