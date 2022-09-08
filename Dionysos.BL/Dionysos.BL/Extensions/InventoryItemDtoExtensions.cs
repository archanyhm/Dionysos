using Dionysos.Dionysos.BL.Dtos;

namespace Dionysos.Dionysos.BL.Extensions;

public static class InventoryItemExtensions
{
    public static Dionysos.Database.Database.InventoryItem ToDbInventoryItem(this InventoryItemDto inventoryItemDto)
    {
        return new Dionysos.Database.Database.InventoryItem
        {
            BestBefore = inventoryItemDto.BestBefore,
            Ean = inventoryItemDto.Ean
        };
    }
}
