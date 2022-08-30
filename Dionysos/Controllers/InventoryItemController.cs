using Dionysos.Database;
using Dionysos.Dtos;
using Dionysos.Services;
using Dionysos.Services.InventoryItemServices;
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
        var items = service.FetchItems();
        return new JsonResult(items);
    }

    [HttpPost]
    public ActionResult PostInventoryItems([FromBody] InventoryItemDto inventoryItemDto)
    {
        var savingService = new InventoryItemSavingService(new MainDbContext());
        savingService.SaveInventoryItem(inventoryItemDto);
        
        return new CreatedResult("", null);
    }
}