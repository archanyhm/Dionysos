using Dionysos.CustomExceptions;
using Dionysos.Database;
using Dionysos.Dtos;
using Dionysos.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Dionysos.Services.InventoryItemServices;

public class InventoryItemSavingService
{
    private readonly IMainDbContext _mainDbContext;

    public InventoryItemSavingService(IMainDbContext mainDbContext)
    {
        _mainDbContext = mainDbContext;
    }

    public void SaveInventoryItem(InventoryItemDto inventoryItemDto)
    {
        if (DoesInventoryItemExist(inventoryItemDto)) throw new ObjectAlreadyExistsException();

        try
        {
            _mainDbContext.InventoryItems.Add(inventoryItemDto.ToDbInventoryItem());
            _mainDbContext.SaveChanges();
        }
        catch (DbUpdateException e)
        {
            ThrowDatabaseException(e);
        }
    }

    public void UpdateInventoryItem(InventoryItemDto inventoryItemDto)
    {
        if (!DoesInventoryItemExist(inventoryItemDto)) throw new ObjectDoesNotExistException();
        try
        {
            _mainDbContext.InventoryItems.Update(inventoryItemDto.ToDbInventoryItem());
            _mainDbContext.SaveChanges();
        }
        catch (DbUpdateException e)
        {
            ThrowDatabaseException(e);
        }
    }

    private bool DoesInventoryItemExist(InventoryItemDto inventoryItemDto)
    {
        return _mainDbContext.InventoryItems.Any(x => x.Id == inventoryItemDto.Id);
    }
    
    private static void ThrowDatabaseException(DbUpdateException e)
    {
        throw new DatabaseException(
            "Beim Aktualisieren der Datenbank trat ein Fehler auf: ",
            e);
    }
}
