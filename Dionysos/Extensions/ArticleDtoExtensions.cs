using Dionysos.Database;
using Dionysos.Dtos;

namespace Dionysos.Extensions;

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

    public static DionysosProtobuf.Article ToProtobufArticle(this ArticleDto articleDto)
    {
        return new DionysosProtobuf.Article
        {
            Ean = articleDto.Ean,
            Description = articleDto.Description,
            Name = articleDto.Name,
            VendorId = articleDto.VendorId
        };
    }
}
