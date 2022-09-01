using Dionysos.Database;
using Dionysos.Dtos;

namespace Dionysos.Services.VendorServices;

public class VendorSavingService
{
    private readonly IMainDbContext _mainDbContext;

    public VendorSavingService(IMainDbContext mainDbContext)
    {
        _mainDbContext = mainDbContext;
    }
    public void SaveVendor(VendorDto vendorDto)
    {
        var isItemAlreadyKnown = _mainDbContext.Vendors.Any(x => x.Id == vendorDto.Id);
        if (!isItemAlreadyKnown)
        {
            var newItem = CreateDbVendor(vendorDto);
            _mainDbContext.Vendors.Add(newItem);
        }
        
        _mainDbContext.SaveChanges();
    }

    private static Vendor CreateDbVendor(VendorDto vendorDto)
    {
        return new Vendor
        {
            Id = vendorDto.Id,
            Name = vendorDto.Name,
            CountryCode = vendorDto.CountryCode
        };
    }

    public void UpdateVendor(VendorDto vendorDto)
    {
        _mainDbContext.Vendors.Update(CreateDbVendor(vendorDto));
        _mainDbContext.SaveChanges();
    }
}