using Dionysos.Database;
using Dionysos.Dtos;

namespace Dionysos.Services;

public class ArticleFetchingService
{
    public List<ArticleDto> FetchArticles()
    {
        var dbContext = new MainDbContext();
        var articles = dbContext.Articles.ToList();

        return articles.Select(CreateArticleDto).ToList();
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