using Dionysos.Dionysos.BL.Dtos;
using Dionysos.Dionysos.Database.Database;

namespace Dionysos.Dionysos.BL.Extensions;

internal static class ArticleDtoExtensions
{
    public static Article ToDbArticle(this ArticleDto articleDto)
    {
        return new Article
        {
            Ean = articleDto.Ean,
            Description = articleDto.Description,
            Name = articleDto.Name,
            VendorId = articleDto.VendorId
        };
    }
}
