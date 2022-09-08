using Dionysos.Dionysos.BL.Dtos;
using DionysosProtobuf;

namespace Dionysos.Dionysos.BL.Extensions;

public static class VendorDtoExtensions
{
    public static Vendor ToProtobufVendor(this VendorDto vendorDto)
    {
        return new Vendor
        {
            Id = vendorDto.Id,
            Name = vendorDto.Name,
            CountryCode = vendorDto.CountryCode
        };
    }
}
