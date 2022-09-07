using Dionysos.Dionysos.BL.Dtos;
using DionysosProtobuf;
using Google.Protobuf.WellKnownTypes;

namespace Dionysos.Dionysos.BL.Extensions;

public static class InventoryItemExtensions
{
    public static InventoryItem ToProtobufItem(this InventoryItemDto itemDto)
    {
        return new InventoryItem
        {
            Id = itemDto.Id,
            BestBefore = Timestamp.FromDateTime(itemDto.BestBefore ?? DateTime.MinValue),
            Ean = itemDto.Ean
        };
    }

    public static Dionysos.Database.Database.InventoryItem ToDbInventoryItem(this InventoryItemDto inventoryItemDto)
    {
        return new Dionysos.Database.Database.InventoryItem
        {
            BestBefore = inventoryItemDto.BestBefore,
            Ean = inventoryItemDto.Ean
        };
    }
}
