using Dionysos.Database;
using Dionysos.Dtos;
using Dionysos.Extensions;

namespace Dionysos.Services.ArticleServices;

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