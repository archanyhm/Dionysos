using Dionysos.BL.Dionysos.BL.Dtos;
using Dionysos.Database.Database;

namespace Dionysos.BL.Dionysos.BL.Extensions;

public static class DbInventoryItemExtensions
{
    public static InventoryItemDto ToInventoryItemDto(this InventoryItem item)
    {
        var newInventoryItemDto = new InventoryItemDto
        {
            Ean = item.Ean,
            Id = item.Id,
            BestBefore = item.BestBefore
        };

        return newInventoryItemDto;
    }
}
