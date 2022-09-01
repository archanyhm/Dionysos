using Dionysos.Database;
using Dionysos.Dtos;

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

        return items.Select(CreateVendorDto).ToList();
    }

    public VendorDto FetchVendor(int id)
    {
        var item = _dbContext.Vendors
            .Where(x => x.Id == id)
            .Select(x => CreateVendorDto(x))
            .SingleOrDefault() ?? new VendorDto();
        return item;
    }
    
    private VendorDto CreateVendorDto(Vendor vendor)
    {
        var newVendorDto = new VendorDto()
        {
            Id = vendor.Id,
            Name = vendor.Name,
            CountryCode = vendor.Name
        };
        
        return newVendorDto;
    }
}