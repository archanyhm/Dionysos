using Dionysos.Dtos;
using DionysosProtobuf;

namespace Dionysos.Extensions;

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

    public static Database.Vendor ToDbVendor(this VendorDto vendorDto)
    {
        return new Database.Vendor
        {
            Id = vendorDto.Id,
            Name = vendorDto.Name,
            CountryCode = vendorDto.CountryCode
        };
    }
}