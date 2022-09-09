using Dionysos.CustomExceptions;
using Dionysos.Database;
using Dionysos.Dtos;
using Dionysos.Extensions;
using Microsoft.EntityFrameworkCore;
using InvalidDataException = Dionysos.CustomExceptions.InvalidDataException;

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
        if (IsForeignKeyValid(inventoryItemDto.Ean)) throw new InvalidDataException();

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
        if (IsForeignKeyValid(inventoryItemDto.Ean)) throw new InvalidDataException();
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

    private bool IsForeignKeyValid(string ean)
    {
        return _mainDbContext.Articles.Any(x => x.Ean == ean);
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
