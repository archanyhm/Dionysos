using Dionysos.Database;
using Dionysos.Dtos;

namespace Dionysos.Services.InventoryItemServices;

public class InventoryItemFetchingService
{
    private MainDbContext _dbContext;

    public InventoryItemFetchingService()
    {
        _dbContext = new MainDbContext();
    }

    public List<InventoryItemDto> FetchItems()
    {
        var items = _dbContext.InventoryItems.ToList();

        return items.Select(CreateInventoryItemDto).ToList();
    }

    public InventoryItemDto FetchItem(int id)
    {
        var item = _dbContext.InventoryItems
            .Where(x => x.Id == id)
            .Select(x => new InventoryItemDto()
            {
                Id = x.Id,
                BestBefore = x.BestBefore,
                Ean = x.Ean,
            })
            .SingleOrDefault();
        return item ?? new InventoryItemDto();
    }
    
    private InventoryItemDto CreateInventoryItemDto(InventoryItem item)
    {
        var newInventoryItemDto = new InventoryItemDto()
        {
            Ean = item.Ean,
            Id = item.Id,
            BestBefore = item.BestBefore
        };
        
        return newInventoryItemDto;
    }
}