using Dionysos.Database;
using Dionysos.Dtos;

namespace Dionysos.Extensions;

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
