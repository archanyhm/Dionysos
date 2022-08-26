using System.Text.Json;
using Dionysos.Database;
using Dionysos.Models;
using Dionysos.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dionysos.Controllers;

[ApiController]
[Route("[controller]")]
public class ArticleController
{
    [HttpGet]
    public JsonResult GetArticles()
    {
        var service = new ArticleFetchingService(new MainDbContext());
        var articles = service.FetchArticles();
        return new JsonResult(articles);
    }

    [HttpPost]
    public ActionResult PostArticle([FromBody] ArticleDto articleDto)
    {
        var savingService = new ArticleSavingService(new MainDbContext());
        savingService.SaveArticle(articleDto);
        
        return new CreatedResult("", null);
    }
}