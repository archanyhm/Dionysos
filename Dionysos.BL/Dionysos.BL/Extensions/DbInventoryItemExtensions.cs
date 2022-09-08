using Dionysos.Dionysos.BL.Dtos;
using Dionysos.Dionysos.Database.Database;

namespace Dionysos.Dionysos.BL.Extensions;

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
