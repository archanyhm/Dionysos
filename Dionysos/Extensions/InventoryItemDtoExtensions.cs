using Dionysos.Dionysos.BL.Dtos;
using DionysosProtobuf;
using Google.Protobuf.WellKnownTypes;

namespace Dionysos.Dionysos.BL.Extensions;

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
