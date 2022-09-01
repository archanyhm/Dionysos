using Dionysos.Database;
using Dionysos.Dtos;
using DionysosProtobuf;
using Google.Protobuf.WellKnownTypes;

namespace Dionysos.Extensions;

public static class InventoryItemExtensions
{
    public static Item ToProtobufItem(this InventoryItemDto itemDto)
    {
        return new Item
        { 
            Id = itemDto.Id,
            BestBefore =  Timestamp.FromDateTime(itemDto.BestBefore ?? DateTime.MinValue),
            Ean = itemDto.Ean,
        };
    }

    public static InventoryItemDto ToInventoryItemDto(this Item protobufItem)
    {
        return new InventoryItemDto()
        { 
            Id = protobufItem.Id,
            BestBefore = protobufItem.BestBefore.ToDateTime(),
            Ean = protobufItem.Ean,
        };
    }

    public static InventoryItemDto ToInventoryItemDto(this InventoryItem item)
    {
        var newInventoryItemDto = new InventoryItemDto()
        {
            Ean = item.Ean,
            Id = item.Id,
            BestBefore = item.BestBefore
        };
        
        return newInventoryItemDto;
    }

    public static InventoryItem ToDbInventoryItem(this InventoryItemDto inventoryItemDto)
    {
        return new InventoryItem
        {
            BestBefore = inventoryItemDto.BestBefore,
            Ean = inventoryItemDto.Ean
        };
    }
}