using Dionysos.CustomExceptions;
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

    public IEnumerable<VendorDto> FetchVendors()
    {
        var vendors = _dbContext.Vendors.ToList();
        if (!vendors.Any()) throw new ObjectDoesNotExistException();

        return vendors.Select(x => x.ToVendorDto()).ToList();
    }

    public VendorDto FetchVendor(int id)
    {
        try
        {
            var vendor = _dbContext.Vendors
                .Where(x => x.Id == id)
                .Select(x => x.ToVendorDto())
                .Single();
            return vendor;
        }
        catch (ArgumentNullException e)
        {
            throw new ObjectDoesNotExistException();
        }
    }
}
