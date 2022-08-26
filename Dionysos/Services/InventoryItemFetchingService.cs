using Dionysos.Database;
using Dionysos.Dtos;

namespace Dionysos.Services;

public class InventoryItemFetchingService
{
    public List<InventoryItemDto> FetchArticles()
    {
        var dbContext = new MainDbContext();
        var items = dbContext.InventoryItems.ToList();

        return items.Select(CreateInventoryItemDto).ToList();
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