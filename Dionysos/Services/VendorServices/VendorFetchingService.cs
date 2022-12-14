using Dionysos.Database;
using Dionysos.Dtos;
using Dionysos.Extensions;

namespace Dionysos.Services.VendorServices;

public class VendorFetchingService
{
    private readonly IMainDbContext _dbContext;

    public VendorFetchingService(IMainDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<VendorDto> FetchVendors()
    {
        var items = _dbContext.Vendors.ToList();

        return items.Select(x => x.ToVendorDto()).ToList();
    }

    public VendorDto FetchVendor(int id)
    {
        var item = _dbContext.Vendors
            .Where(x => x.Id == id)
            .Select(x => x.ToVendorDto())
            .Single();
        return item;
    }
}
