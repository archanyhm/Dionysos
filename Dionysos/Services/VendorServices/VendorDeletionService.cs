using Dionysos.Database;

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
        var vendor = _dbContext.Vendors.Single(x => x.Id == id);
        _dbContext.Vendors.Remove(vendor);
        _dbContext.SaveChanges();
    }
}