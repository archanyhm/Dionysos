using Dionysos.Dtos;
using DionysosProtobuf;
using Google.Protobuf.WellKnownTypes;

namespace Dionysos.Extensions;

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

    public static Database.InventoryItem ToDbInventoryItem(this InventoryItemDto inventoryItemDto)
    {
        return new Database.InventoryItem
        {
            BestBefore = inventoryItemDto.BestBefore,
            Ean = inventoryItemDto.Ean
        };
    }
}