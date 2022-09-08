using Dionysos.BL.Dionysos.BL.Dtos;

namespace Dionysos.Extensions;

public static class ArticleDtoExtensions
{
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
