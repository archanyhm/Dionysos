using Dionysos.Database;
using Dionysos.Dtos;
using Dionysos.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dionysos.Controllers;

[ApiController]
[Route("[controller]")]
public class InventoryItemController
{
    [HttpGet]
    public JsonResult GetInventoryItems()
    {
        var service = new InventoryItemFetchingService();
        var items = service.FetchArticles();
        return new JsonResult(items);
    }

    [HttpPost]
    public ActionResult PostInventoryItems([FromBody] InventoryItemDto inventoryItemDto)
    {
        var savingService = new InventoryItemSavingService(new MainDbContext());
        savingService.SaveArticle(inventoryItemDto);
        
        return new CreatedResult("", null);
    }
}