using Dionysos.Database;
using Dionysos.Dtos;
using Dionysos.Services.ArticleServices;
using DionysosProtobuf;
using Grpc.Core;
using Article = DionysosProtobuf.Article;

namespace Dionysos.GrpcService;

public class ArticleCrudService : DionysosProtobuf.ArticleCrudService.ArticleCrudServiceBase
{
    private readonly MainDbContext _mainDbContext = new MainDbContext();

    public override Task<BooleanReply> CreateArticle(Article request, ServerCallContext context)
    {
        var articleSavingService = new ArticleSavingService(_mainDbContext);
        articleSavingService.SaveArticle(ArticleDtoToProtobufArticle(request));
        return CreateSuccessResult();
    }

    public override Task<Article> ReadArticle(SimpleArticle request, ServerCallContext context)
    {
        var articleFetchingService = new ArticleFetchingService(_mainDbContext);
        var articleDto = articleFetchingService.FetchArticle(request.Ean);
        var protobufArticle = ArticleDtoToProtobufArticle(articleDto);
        return Task.FromResult(protobufArticle);
    }

    public override Task<ArticlesReply> GetAllArticles(EmptyRequest request, ServerCallContext context)
    {
        var articleFetchingService = new ArticleFetchingService(_mainDbContext);
        var articleResultList = articleFetchingService
            .FetchArticles()
            .Select(ArticleDtoToProtobufArticle)
            .ToList();

        return Task.FromResult(new ArticlesReply{Articles = { articleResultList }});
    }
    
    public override Task<BooleanReply> UpdateArticle(Article request, ServerCallContext context)
    {
        var articleSavingService = new ArticleSavingService(_mainDbContext);
        articleSavingService.UpdateArticle(ArticleDtoToProtobufArticle(request));
        return CreateSuccessResult();
    }

    public override Task<BooleanReply> DeleteArticle(SimpleArticle request, ServerCallContext context)
    {
        var service = new ArticleDeletionService(_mainDbContext);
        service.DeleteArticle(request.Ean);
        
        return CreateSuccessResult();
    }
    
    private static Task<BooleanReply> CreateSuccessResult()
    {
        return Task.FromResult(new BooleanReply{Success = true});
    }
    
    private static Article ArticleDtoToProtobufArticle(ArticleDto articleDto)
    {
        return new Article 
        { 
            Ean = articleDto.Ean, 
            Name = articleDto.Name, 
            Description = articleDto.Description, 
            VendorId = articleDto.VendorId 
        };
    }
    
    private static ArticleDto ArticleDtoToProtobufArticle(Article protobufArticle)
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
