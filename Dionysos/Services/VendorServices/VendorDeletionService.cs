using System.Linq;
using Dionysos.CustomExceptions;
using Dionysos.Database;
using Microsoft.EntityFrameworkCore;

namespace Dionysos.Services.VendorServices;

public class VendorDeletionService
{
    private readonly IMainDbContext _dbContext;

    public VendorDeletionService(IMainDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void DeleteVendor(int id)
    {
        try
        {
            var vendor = _dbContext.Vendors.SingleOrDefault(x => x.Id == id);
            
            if (vendor is null) throw new ObjectDoesNotExistException();
            _dbContext.Vendors.Remove(vendor);
            _dbContext.SaveChanges();
        }
        catch (ArgumentNullException e)
        {
            throw new MultipleEntriesFoundException();
        }
        catch (DbUpdateException e)
        {
            throw new DatabaseException(
                "Beim Aktualisieren der Datenbank trat ein Fehler auf: ",
                e);
        }
    }
}
