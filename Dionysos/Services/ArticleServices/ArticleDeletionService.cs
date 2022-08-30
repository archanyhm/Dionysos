using System.Data;
using Dionysos.Database;
using Microsoft.EntityFrameworkCore;

namespace Dionysos.Services.ArticleServices;

public class ArticleDeletionService
{
    private MainDbContext _dbContext;

    public ArticleDeletionService()
    {
        _dbContext = new MainDbContext();
    }

    public void DeleteArticle(string ean)
    {
        var article = _dbContext.Articles
            .Single(x => x.Ean == ean);

        _dbContext.Articles.Remove(article);
        _dbContext.SaveChanges();
    }
}