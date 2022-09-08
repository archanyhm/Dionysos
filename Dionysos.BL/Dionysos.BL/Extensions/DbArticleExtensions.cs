using Dionysos.BL.Dionysos.BL.Dtos;
using Dionysos.Database.Database;

namespace Dionysos.BL.Dionysos.BL.Extensions;

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
