using Dionysos.BL.Dionysos.BL.Dtos;

namespace Dionysos.BL.Dionysos.BL.Extensions;

public static class VendorExtensions
{
    public static Database.Database.Vendor ToDbVendor(this VendorDto vendorDto)
    {
        return new Database.Database.Vendor
        {
            Id = vendorDto.Id,
            Name = vendorDto.Name,
            CountryCode = vendorDto.CountryCode
        };
    }
}
