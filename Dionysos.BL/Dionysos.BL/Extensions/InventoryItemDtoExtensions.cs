using Dionysos.BL.Dionysos.BL.Dtos;
using Dionysos.Database.Database;

namespace Dionysos.BL.Dionysos.BL.Extensions;

public static class InventoryItemExtensions
{
    public static InventoryItem ToDbInventoryItem(this InventoryItemDto inventoryItemDto)
    {
        return new InventoryItem
        {
            BestBefore = inventoryItemDto.BestBefore,
            Ean = inventoryItemDto.Ean
        };
    }
}
