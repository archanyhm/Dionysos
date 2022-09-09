using Dionysos.CustomExceptions;
using Dionysos.Database;
using Dionysos.Dtos;
using Dionysos.Extensions;
using Microsoft.EntityFrameworkCore;

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
        if (DoesVendorExist(vendorDto)) throw new ObjectAlreadyExistsException();
        try
        {
            _mainDbContext.Vendors.Add(vendorDto.ToDbVendor());
            _mainDbContext.SaveChanges();
        }
        catch (DbUpdateException e)
        {
            ThrowDatabaseException(e);
        }
    }

    public void UpdateVendor(VendorDto vendorDto)
    {
        if (!DoesVendorExist(vendorDto)) throw new ObjectDoesNotExistException();
        try
        {
            _mainDbContext.Vendors.Update(vendorDto.ToDbVendor());
            _mainDbContext.SaveChanges();
        }
        catch (DbUpdateException e)
        {
            ThrowDatabaseException(e);
        }
    }

    private bool DoesVendorExist(VendorDto vendorDto)
    {
        return _mainDbContext.Vendors.Any(x => x.Id == vendorDto.Id);
    }
    
    private static void ThrowDatabaseException(DbUpdateException e)
    {
        throw new DatabaseException(
            "Beim Aktualisieren der Datenbank trat ein Fehler auf: ",
            e);
    }
}
