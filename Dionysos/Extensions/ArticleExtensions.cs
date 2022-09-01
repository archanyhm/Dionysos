using Dionysos.Dtos;
using DionysosProtobuf;

namespace Dionysos.Extensions;

public static class ArticleExtensions
{
    public static DionysosProtobuf.Article ToProtobufArticle(this ArticleDto articleDto)
    {
        return new Article 
        { 
            Ean = articleDto.Ean, 
            Name = articleDto.Name, 
            Description = articleDto.Description, 
            VendorId = articleDto.VendorId 
        };
    }

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


    public static ArticleDto ToArticleDto(this Database.Article article)
    {
        return new ArticleDto
        {
            Ean = article.Ean,
            Description = article.Description,
            VendorId = article.VendorId,
            Name = article.Name
        };
    }

    public static Database.Article ToDbArticle(this ArticleDto articleDto)
    {
        return new Database.Article
        {
            Ean = articleDto.Ean,
            Description = articleDto.Description,
            Name = articleDto.Name,
            VendorId = articleDto.VendorId
        };
    }
}