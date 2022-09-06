using Dionysos.CustomExceptions;
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

    public IEnumerable<ArticleDto> FetchArticles()
    {
        var articles = _dbContext.Articles.ToList();
        if (!articles.Any()) throw new ObjectDoesNotExistException();

        return articles.Select(x => x.ToArticleDto()).ToList();
    }

    public ArticleDto FetchArticle(string ean)
    {
        try
        {
            var article = _dbContext.Articles
                .Where(x => x.Ean == ean)
                .Select(x => x.ToArticleDto())
                .Single();

            return article;
        }
        catch (ArgumentNullException e)
        {
            throw new ObjectDoesNotExistException();
        }
    }
}
