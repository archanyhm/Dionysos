using Dionysos.Dtos;

namespace Dionysos.Extensions;

public static class VendorExtensions
{
    public static VendorDto ToVendorDto(this DionysosProtobuf.Vendor vendor)
    {
        return new VendorDto
        {
            Id = vendor.Id,
            Name = vendor.Name,
            CountryCode = vendor.Name
        };
    }
    
    public static VendorDto ToVendorDto(this Database.Vendor vendor)
    {
        return new VendorDto
        {
            Id = vendor.Id,
            Name = vendor.Name,
            CountryCode = vendor.Name
        };
    }

    public static DionysosProtobuf.Vendor ToProtobufVendor(this VendorDto vendorDto)
    {
        return new DionysosProtobuf.Vendor
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