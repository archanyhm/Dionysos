using Dionysos.Database;

namespace Dionysos.Services.ArticleServices;

public class ArticleDeletionService
{
    private readonly IMainDbContext _dbContext;

    public ArticleDeletionService(IMainDbContext mainDbContext)
    {
        _dbContext = mainDbContext;
    }

    public void DeleteArticle(string ean)
    {
        var article = _dbContext.Articles
            .Single(x => x.Ean == ean);

        _dbContext.Articles.Remove(article);
        _dbContext.SaveChanges();
    }
}