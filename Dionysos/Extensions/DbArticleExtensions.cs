using Dionysos.Database;
using Dionysos.Dtos;

namespace Dionysos.Extensions;

public static class ArticleExtensions
{
    public static ArticleDto ToArticleDto(this Article article)
    {
        return new ArticleDto
        {
            Ean = article.Ean,
            Description = article.Description,
            VendorId = article.VendorId,
            Name = article.Name
        };
    }
}