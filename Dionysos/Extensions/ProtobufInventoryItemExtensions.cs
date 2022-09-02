using Dionysos.Dtos;
using DionysosProtobuf;

namespace Dionysos.Extensions;

public static class ProtobufItemExtensions
{
    public static InventoryItemDto ToInventoryItemDto(this InventoryItem protobufItem)
    {
        return new InventoryItemDto
        {
            Id = protobufItem.Id,
            BestBefore = protobufItem.BestBefore.ToDateTime(),
            Ean = protobufItem.Ean
        };
    }
}