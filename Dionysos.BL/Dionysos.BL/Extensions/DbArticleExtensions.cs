using Dionysos.Dionysos.BL.Dtos;
using Dionysos.Dionysos.Database.Database;

namespace Dionysos.Dionysos.BL.Extensions;

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
