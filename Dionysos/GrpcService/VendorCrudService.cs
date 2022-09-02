using Dionysos.Database;
using Dionysos.Extensions;
using Dionysos.Services.VendorServices;
using DionysosProtobuf;
using Grpc.Core;
using Vendor = DionysosProtobuf.Vendor;

namespace Dionysos.GrpcService;

public class VendorCrudService : DionysosProtobuf.VendorCrudService.VendorCrudServiceBase
{
    private readonly MainDbContext _mainDbContext;

    public VendorCrudService(MainDbContext mainDbContext)
    {
        _mainDbContext = mainDbContext;
    }

    public override Task<BooleanReply> CreateVendor(Vendor request, ServerCallContext context)
    {
        var service = new VendorSavingService(_mainDbContext);
        service.SaveVendor(request.ToVendorDto());

        return CreateSuccessResult();
    }

    public override Task<Vendor> ReadVendor(SimpleVendor request, ServerCallContext context)
    {
        var vendor = new VendorFetchingService(_mainDbContext)
            .FetchVendor(request.Id)
            .ToProtobufVendor();
        return Task.FromResult(vendor);
    }

    public override Task<Vendors> GetAllVendors(EmptyRequest request, ServerCallContext context)
    {
        var vendors = new VendorFetchingService(_mainDbContext)
            .FetchVendors()
            .Select(x => x.ToProtobufVendor())
            .ToList();
        return Task.FromResult(new Vendors { Values = {vendors } });
    }

    public override Task<BooleanReply> UpdateVendor(Vendor request, ServerCallContext context)
    {
        new VendorSavingService(_mainDbContext).SaveVendor(request.ToVendorDto());
        return CreateSuccessResult();
    }

    public override Task<BooleanReply> DeleteVendor(SimpleVendor request, ServerCallContext context)
    {
        new VendorDeletionService(_mainDbContext).DeleteVendor(request.Id);
        return base.DeleteVendor(request, context);
    }

    private static Task<BooleanReply> CreateSuccessResult()
    {
        return Task.FromResult(new BooleanReply { Success = true });
    }
}