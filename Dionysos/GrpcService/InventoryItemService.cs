using Dionysos.Database;
using Dionysos.Extensions;
using Dionysos.Services.InventoryItemServices;
using DionysosProtobuf;
using Grpc.Core;
using InventoryItem = DionysosProtobuf.InventoryItem;

namespace Dionysos.GrpcService;

public class InventoryItemService : DionysosProtobuf.InventoryItemService.InventoryItemServiceBase
{
    private readonly MainDbContext _mainDbContext;

    public InventoryItemService(MainDbContext mainDbContext)
    {
        _mainDbContext = mainDbContext;
    }

    public override Task<BooleanReply> CreateInventoryItem(InventoryItem request, ServerCallContext context)
    {
        var service = new InventoryItemSavingService(_mainDbContext);
        service.SaveInventoryItem(request.ToInventoryItemDto());

        return CreateSuccessResult();
    }

    public override Task<InventoryItem> ReadInventoryItem(SimpleInventoryItem request, ServerCallContext context)
    {
        var itemDto = new InventoryItemFetchingService(_mainDbContext).FetchItem(request.Id);
        var protobufItem = itemDto.ToProtobufItem();
        return Task.FromResult(protobufItem);
    }

    public override Task<InventoryItems> GetAllInventoryItems(EmptyRequest request, ServerCallContext context)
    {
        var service = new InventoryItemFetchingService(_mainDbContext);
        var items = service.FetchItems().Select(x => x.ToProtobufItem()).ToList();
        return Task.FromResult(new InventoryItems { Values = { items } });
    }

    public override Task<BooleanReply> UpdateInventoryItem(InventoryItem request, ServerCallContext context)
    {
        var service = new InventoryItemSavingService(_mainDbContext);
        service.UpdateInventoryItem(request.ToInventoryItemDto());
        return CreateSuccessResult();
    }

    public override Task<BooleanReply> DeleteInventoryItem(SimpleInventoryItem request, ServerCallContext context)
    {
        new InventoryItemDeletingService(_mainDbContext).DeleteInventoryItem(request.Id);
        return CreateSuccessResult();
    }

    private static Task<BooleanReply> CreateSuccessResult()
    {
        return Task.FromResult(new BooleanReply { Success = true });
    }
}
