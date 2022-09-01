using Dionysos.Database;
using Dionysos.Dtos;
using Dionysos.Extensions;

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
            var newItem = inventoryItemDto.ToDbInventoryItem();
            _mainDbContext.InventoryItems.Add(newItem);
        }
        
        _mainDbContext.SaveChanges();
    }

    public void UpdateInventoryItem(InventoryItemDto inventoryItemDto)
    {
        _mainDbContext.InventoryItems.Update(inventoryItemDto.ToDbInventoryItem());
        _mainDbContext.SaveChanges();
    }
}