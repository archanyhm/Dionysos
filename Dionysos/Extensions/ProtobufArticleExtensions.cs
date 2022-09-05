using Dionysos.Dtos;
using DionysosProtobuf;

namespace Dionysos.Extensions;

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
