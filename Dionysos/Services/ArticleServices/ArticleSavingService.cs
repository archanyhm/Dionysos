using Dionysos.Database;
using Dionysos.Dtos;
using Dionysos.Extensions;

namespace Dionysos.Services.ArticleServices;

public class ArticleSavingService
{
    private readonly IMainDbContext _dbContext;
    public ArticleSavingService(IMainDbContext mainDbContext)
    {
        _dbContext = mainDbContext;
    }
    public void SaveArticle(ArticleDto articleToAdd)
    {
        var articleAlreadyKnown = _dbContext.Articles.Any(x => x.Ean == articleToAdd.Ean);
        if (!articleAlreadyKnown)
        {
            var newArticle = articleToAdd.ToDbArticle();
            _dbContext.Articles.Add(newArticle);
        }
        _dbContext.SaveChanges();
    }

    public void UpdateArticle(ArticleDto articleToChange)
    {
        _dbContext.Articles.Update(articleToChange.ToDbArticle());
        _dbContext.SaveChanges();
    }
}