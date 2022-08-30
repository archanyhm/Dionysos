using System.Text.Json;
using Dionysos.Dtos;
using Dionysos.Services;
using Dionysos.Services.ArticleServices;
using Microsoft.AspNetCore.Mvc;

namespace Dionysos.Controllers;

[ApiController]
[Route("[controller]")]
public class ArticleController
{
    [HttpGet]
    public JsonResult GetArticles()
    {
        var service = new ArticleFetchingService();
        var articles = service.FetchArticles();
        return new JsonResult(articles);
    }

    [HttpPost]
    public ActionResult PostArticle([FromBody] ArticleDto articleDto)
    {
        var savingService = new ArticleSavingService();
        savingService.SaveArticle(articleDto);
        
        return new CreatedResult("", null);
    }
}