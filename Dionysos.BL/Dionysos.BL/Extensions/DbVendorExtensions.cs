using Dionysos.BL.Dionysos.BL.Dtos;
using Dionysos.Database.Database;

namespace Dionysos.BL.Dionysos.BL.Extensions;

public static class DbVendorExtensions
{
    public static VendorDto ToVendorDto(this Vendor vendor)
    {
        return new VendorDto
        {
            Id = vendor.Id,
            Name = vendor.Name,
            CountryCode = vendor.Name
        };
    }
}
