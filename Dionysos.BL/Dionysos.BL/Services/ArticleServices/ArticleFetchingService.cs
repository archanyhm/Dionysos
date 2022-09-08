using Dionysos.BL.Dionysos.BL.Dtos;
using Dionysos.BL.Dionysos.BL.Extensions;
using Dionysos.Database.Database;

namespace Dionysos.BL.Dionysos.BL.Services.ArticleServices;

public class ArticleFetchingService
{
    private readonly IMainDbContext _dbContext;

    public ArticleFetchingService(IMainDbContext mainDbContext)
    {
        _dbContext = mainDbContext;
    }

    public List<ArticleDto> FetchArticles()
    {
        var articles = _dbContext.Articles.ToList();

        return articles.Select(x => x.ToArticleDto()).ToList();
    }

    public ArticleDto FetchArticle(string ean)
    {
        var article = _dbContext.Articles
            .Where(x => x.Ean == ean)
            .Select(x => x.ToArticleDto())
            .Single();

        return article;
    }
}
