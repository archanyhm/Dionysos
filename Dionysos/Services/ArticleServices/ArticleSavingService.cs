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
        if (!DoesArticleExist(articleToAdd))
        {
            var newArticle = articleToAdd.ToDbArticle();
            _dbContext.Articles.Add(newArticle);
            _dbContext.SaveChanges();
        }

    }

    public void UpdateArticle(ArticleDto articleToChange)
    {
        if (DoesArticleExist(articleToChange))
        {
            _dbContext.Articles.Update(articleToChange.ToDbArticle());
            _dbContext.SaveChanges();
        }
    }

    private bool DoesArticleExist(ArticleDto articleToAdd)
    {
        return _dbContext.Articles.Any(x => x.Ean == articleToAdd.Ean);
    }
}