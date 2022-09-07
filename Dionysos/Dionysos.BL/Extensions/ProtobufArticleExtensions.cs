using Dionysos.Dionysos.BL.Dtos;
using DionysosProtobuf;

namespace Dionysos.Dionysos.BL.Extensions;

public static class ProtobufArticleExtensions
{
    public static ArticleDto ToArticleDto(this Article protobufArticle)
    {
        return new ArticleDto
        {
            Ean = protobufArticle.Ean,
            Name = protobufArticle.Name,
            Description = protobufArticle.Description,
            VendorId = protobufArticle.VendorId
        };
    }
}
