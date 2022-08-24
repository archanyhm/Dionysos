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
        var service = new ArticleFetchingService();
        var articles = service.FetchArticles();
        return new JsonResult(articles);
    }
}