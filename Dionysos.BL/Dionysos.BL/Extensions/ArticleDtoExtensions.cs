using Dionysos.BL.Dionysos.BL.Dtos;
using Dionysos.Database.Database;

namespace Dionysos.BL.Dionysos.BL.Extensions;

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
