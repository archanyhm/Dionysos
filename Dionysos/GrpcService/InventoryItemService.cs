using Dionysos.CustomExceptions;
using Dionysos.Database;
using Dionysos.Extensions;
using Dionysos.Services.InventoryItemServices;
using DionysosProtobuf;
using Grpc.Core;
using InvalidDataException = Dionysos.CustomExceptions.InvalidDataException;
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
        try
        {
            var service = new InventoryItemSavingService(_mainDbContext);
            service.SaveInventoryItem(request.ToInventoryItemDto());

            return CreateSuccessResult();
        }
        catch (InvalidDataException)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, ""));
        }
        catch (ObjectAlreadyExistsException)
        {
            throw new RpcException(new Status(StatusCode.AlreadyExists, ""));
        }
        catch (DatabaseException)
        {
            throw new RpcException(new Status(StatusCode.Aborted, ""));
        }
        catch (Exception)
        {
            throw new RpcException(new Status(StatusCode.Internal, ""));
        }
    }

    public override Task<InventoryItem> ReadInventoryItem(SimpleInventoryItem request, ServerCallContext context)
    {
        try
        {
            var itemDto = new InventoryItemFetchingService(_mainDbContext).FetchItem(request.Id);
            var protobufItem = itemDto.ToProtobufItem();
            return Task.FromResult(protobufItem);
        }
        catch (ObjectDoesNotExistException)
        {
            throw new RpcException(new Status(StatusCode.NotFound, ""));
        }
        catch (Exception)
        {
            throw new RpcException(new Status(StatusCode.Internal, ""));
        }
    }

    public override Task<InventoryItems> GetAllInventoryItems(EmptyRequest request, ServerCallContext context)
    {
        try
        {
            var service = new InventoryItemFetchingService(_mainDbContext);
            var items = service.FetchItems().Select(x => x.ToProtobufItem()).ToList();
            return Task.FromResult(new InventoryItems { Values = { items } });
        }
        catch (ObjectDoesNotExistException)
        {
            throw new RpcException(new Status(StatusCode.NotFound, ""));
        }
        catch (Exception)
        {
            throw new RpcException(new Status(StatusCode.Internal, ""));
        }
    }

    public override Task<BooleanReply> UpdateInventoryItem(InventoryItem request, ServerCallContext context)
    {
        try
        {
            var service = new InventoryItemSavingService(_mainDbContext);
            service.UpdateInventoryItem(request.ToInventoryItemDto());
            return CreateSuccessResult();
        }
        catch (InvalidDataException)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, ""));
        }
        catch (ObjectDoesNotExistException)
        {
            throw new RpcException(new Status(StatusCode.NotFound, ""));
        }
        catch (DatabaseException)
        {
            throw new RpcException(new Status(StatusCode.Aborted, ""));
        }
        catch (Exception)
        {
            throw new RpcException(new Status(StatusCode.Internal, ""));
        }
    }

    public override Task<BooleanReply> DeleteInventoryItem(SimpleInventoryItem request, ServerCallContext context)
    {
        try
        {
            new InventoryItemDeletingService(_mainDbContext).DeleteInventoryItem(request.Id);
            return CreateSuccessResult();
        }
        catch (ObjectDoesNotExistException)
        {
            throw new RpcException(new Status(StatusCode.NotFound, ""));
        }
        catch (MultipleEntriesFoundException)
        {
            throw new RpcException(new Status(StatusCode.Internal, ""));
        }
        catch (DatabaseException)
        {
            throw new RpcException(new Status(StatusCode.Aborted, ""));
        }
        catch (Exception)
        {
            throw new RpcException(new Status(StatusCode.Internal, ""));
        }
    }

    private static Task<BooleanReply> CreateSuccessResult()
    {
        return Task.FromResult(new BooleanReply { Success = true });
    }
}
