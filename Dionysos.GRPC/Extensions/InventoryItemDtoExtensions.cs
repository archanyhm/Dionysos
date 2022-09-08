using Dionysos.BL.Dionysos.BL.Dtos;
using DionysosProtobuf;
using Google.Protobuf.WellKnownTypes;

namespace Dionysos.Extensions;

public static class InventoryItemDtoExtensions
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
}
