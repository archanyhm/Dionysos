using Dionysos.CustomExceptions;
using Dionysos.Database;
using Dionysos.Extensions;
using Dionysos.Services.VendorServices;
using DionysosProtobuf;
using Grpc.Core;
using InvalidDataException = Dionysos.CustomExceptions.InvalidDataException;
using Vendor = DionysosProtobuf.Vendor;

namespace Dionysos.GrpcService;

public class VendorService : DionysosProtobuf.VendorService.VendorServiceBase
{
    private readonly MainDbContext _mainDbContext;

    public VendorService(MainDbContext mainDbContext)
    {
        _mainDbContext = mainDbContext;
    }

    public override Task<BooleanReply> CreateVendor(Vendor request, ServerCallContext context)
    {
        try
        {
            var service = new VendorSavingService(_mainDbContext);
            service.SaveVendor(request.ToVendorDto());

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

    public override Task<Vendor> ReadVendor(SimpleVendor request, ServerCallContext context)
    {
        try
        {
            var vendor = new VendorFetchingService(_mainDbContext)
                .FetchVendor(request.Id)
                .ToProtobufVendor();
            return Task.FromResult(vendor);
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

    public override Task<Vendors> GetAllVendors(EmptyRequest request, ServerCallContext context)
    {
        try
        {
            var vendors = new VendorFetchingService(_mainDbContext)
                .FetchVendors()
                .Select(x => x.ToProtobufVendor())
                .ToList();
            return Task.FromResult(new Vendors { Values = { vendors } });
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

    public override Task<BooleanReply> UpdateVendor(Vendor request, ServerCallContext context)
    {
        try
        {
            new VendorSavingService(_mainDbContext).SaveVendor(request.ToVendorDto());
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

    public override Task<BooleanReply> DeleteVendor(SimpleVendor request, ServerCallContext context)
    {
        try
        {
            new VendorDeletionService(_mainDbContext).DeleteVendor(request.Id);
            return base.DeleteVendor(request, context);
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
