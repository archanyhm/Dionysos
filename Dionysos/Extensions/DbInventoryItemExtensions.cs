using Dionysos.Database;
using Dionysos.Dtos;

namespace Dionysos.Extensions;

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