using Dionysos.Database;
using Dionysos.Dtos;

namespace Dionysos.Services.ArticleServices;

public class ArticleFetchingService
{
    private MainDbContext _dbContext;

    public ArticleFetchingService()
    {
        _dbContext = new MainDbContext();
    }

    public List<ArticleDto> FetchArticles()
    {
        var articles = _dbContext.Articles.ToList();

        return articles.Select(CreateArticleDto).ToList();
    }

    public ArticleDto FetchArticle(string ean)
    {
        var article = _dbContext.Articles
            .Where(x => x.Ean == ean)
            .Select(x => new ArticleDto
            {
                Ean = x.Ean,
                Name = x.Name,
                Description = x.Description,
                Vendor = x.Vendor
            })
            .SingleOrDefault();
        return article ?? new ArticleDto();
    }

    private ArticleDto CreateArticleDto(Article article)
    {
        var newDto = new ArticleDto
        {
            Ean = article.Ean,
            Description = article.Description,
            Vendor = article.Vendor,
            Name = article.Name
        };
        return newDto;
    }
}