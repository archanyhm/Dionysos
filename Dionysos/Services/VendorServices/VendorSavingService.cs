using Dionysos.Database;
using Dionysos.Dtos;
using Dionysos.Extensions;

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
        if (!DoesVendorExist(vendorDto))
        {
            var newItem = vendorDto.ToDbVendor();
            _mainDbContext.Vendors.Add(newItem);
            _mainDbContext.SaveChanges();
        }
    }

    public void UpdateVendor(VendorDto vendorDto)
    {
        if (DoesVendorExist(vendorDto))
        {
            _mainDbContext.Vendors.Update(vendorDto.ToDbVendor());
            _mainDbContext.SaveChanges();
        }
    }

    private bool DoesVendorExist(VendorDto vendorDto)
    {
        return _mainDbContext.Vendors.Any(x => x.Id == vendorDto.Id);
    }
}
