using Dionysos.Dionysos.BL.Dtos;
using Dionysos.Dionysos.Database.Database;

namespace Dionysos.Dionysos.BL.Extensions;

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
