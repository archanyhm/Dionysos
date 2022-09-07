using Dionysos.Dionysos.BL.Dtos;
using DionysosProtobuf;

namespace Dionysos.Dionysos.BL.Extensions;

public static class VendorExtensions
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
