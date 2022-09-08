using Dionysos.Dionysos.BL.Dtos;
using DionysosProtobuf;

namespace Dionysos.Dionysos.BL.Extensions;

public static class ProtobufVendorExtensions
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
