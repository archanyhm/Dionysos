using Dionysos.Database;
using Dionysos.Dtos;
using Dionysos.Extensions;
using Dionysos.Services.InventoryItemServices;
using DionysosProtobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace Dionysos.GrpcService;

public class InventoryItemCrudService : DionysosProtobuf.ItemCrudService.ItemCrudServiceBase
{
    private readonly MainDbContext _mainDbContext = new MainDbContext();

    public override Task<BooleanReply> CreateItem(Item request, ServerCallContext context)
    {
        var service = new InventoryItemSavingService(_mainDbContext);
        service.SaveInventoryItem(request.ToInventoryItemDto());
        
        return CreateSuccessResult();
    }

    public override Task<Item> ReadItem(SimpleItem request, ServerCallContext context)
    {
        var itemDto = new InventoryItemFetchingService(_mainDbContext).FetchItem(request.Id);
        var protobufItem = itemDto.ToProtobufItem();
        return Task.FromResult(protobufItem);
    }

    public override Task<ItemsReply> GetAllItems(EmptyRequest request, ServerCallContext context)
    {
        var service = new InventoryItemFetchingService(_mainDbContext);
        var items = service.FetchItems().Select(x => x.ToProtobufItem()).ToList();
        return Task.FromResult(new ItemsReply{Items = { items }});
    }

    public override Task<BooleanReply> UpdateItem(Item request, ServerCallContext context)
    {
        var service = new InventoryItemSavingService(_mainDbContext);
        service.UpdateInventoryItem(request.ToInventoryItemDto());
        return CreateSuccessResult();
    }

    public override Task<BooleanReply> DeleteItem(SimpleItem request, ServerCallContext context)
    {
        new InventoryItemDeletingService(_mainDbContext).DeleteInventoryItem(request.Id);
        return CreateSuccessResult();
    }
    
    private static Task<BooleanReply> CreateSuccessResult()
    {
        return Task.FromResult(new BooleanReply{Success = true});
    }
}
