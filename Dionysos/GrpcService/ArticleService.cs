using Dionysos.Database;
using Dionysos.Extensions;
using Dionysos.Services.ArticleServices;
using DionysosProtobuf;
using Grpc.Core;
using Article = DionysosProtobuf.Article;

namespace Dionysos.GrpcService;

public class ArticleService : DionysosProtobuf.ArticleService.ArticleServiceBase
{
    private readonly MainDbContext _mainDbContext;

    public ArticleService(MainDbContext mainDbContext)
    {
        _mainDbContext = mainDbContext;
    }

    public override Task<BooleanReply> CreateArticle(Article request, ServerCallContext context)
    {
        var articleSavingService = new ArticleSavingService(_mainDbContext);
        articleSavingService.SaveArticle(request.ToArticleDto());
        return CreateSuccessResult();
    }

    public override Task<Article> ReadArticle(SimpleArticle request, ServerCallContext context)
    {
        var articleFetchingService = new ArticleFetchingService(_mainDbContext);
        var articleDto = articleFetchingService.FetchArticle(request.Ean);
        var protobufArticle = articleDto.ToProtobufArticle();
        return Task.FromResult(protobufArticle);
    }

    public override Task<Articles> GetAllArticles(EmptyRequest request, ServerCallContext context)
    {
        var articleFetchingService = new ArticleFetchingService(_mainDbContext);
        var articleResultList = articleFetchingService
            .FetchArticles()
            .Select(x => x.ToProtobufArticle())
            .ToList();

        return Task.FromResult(new Articles { Values = { articleResultList } });
    }

    public override Task<BooleanReply> UpdateArticle(Article request, ServerCallContext context)
    {
        var articleSavingService = new ArticleSavingService(_mainDbContext);
        articleSavingService.UpdateArticle(request.ToArticleDto());
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
        return Task.FromResult(new BooleanReply { Success = true });
    }
}
