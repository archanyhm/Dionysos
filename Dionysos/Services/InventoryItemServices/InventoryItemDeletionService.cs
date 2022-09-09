using System.Linq;
using Dionysos.CustomExceptions;
using Dionysos.Database;
using Microsoft.EntityFrameworkCore;

namespace Dionysos.Services.InventoryItemServices;

public class InventoryItemDeletingService
{
    private readonly IMainDbContext _dbContext;

    public InventoryItemDeletingService(IMainDbContext mainDbContext)
    {
        _dbContext = mainDbContext;
    }

    public void DeleteInventoryItem(int id)
    {
        try
        {
            var item = _dbContext.InventoryItems.SingleOrDefault(x => x.Id == id);
            if (item is null) throw new ObjectDoesNotExistException();

            _dbContext.InventoryItems.Remove(item);
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
