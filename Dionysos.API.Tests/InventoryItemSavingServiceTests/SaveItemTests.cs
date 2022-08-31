using System.Collections.Generic;
using System.Linq;
using Dionysos.Database;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Dionysos.API.Tests.InventoryItemSavingServiceTests;

public class SaveItemTests
{
    private List<InventoryItem> _itemsAsList;

    #region DbMock

    private DbSet<InventoryItem> SetupInventoryItemsMock(params InventoryItem[] items)
    {
        _itemsAsList = items.ToList();
        var data = items.AsQueryable();
        
        var itemDbSetMock = new Mock<DbSet<InventoryItem>>();
        itemDbSetMock.As<IQueryable<InventoryItem>>().Setup(m => m.Provider).Returns(data.Provider);
        itemDbSetMock.As<IQueryable<InventoryItem>>().Setup(m => m.Expression).Returns(data.Expression);
        itemDbSetMock.As<IQueryable<InventoryItem>>().Setup(m => m.ElementType).Returns(data.ElementType);
        itemDbSetMock.As<IQueryable<InventoryItem>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
        itemDbSetMock.Setup(d => d.Add(It.IsAny<InventoryItem>())).Callback<InventoryItem>((s) => _itemsAsList.Add(s));
    
        return itemDbSetMock.Object;
    }

    #endregion
    
}