using Dionysos.CustomExceptions;
using Dionysos.Database;
using Dionysos.Extensions;
using Dionysos.Services.ArticleServices;
using DionysosProtobuf;
using Grpc.Core;
using Article = DionysosProtobuf.Article;
using InvalidDataException = Dionysos.CustomExceptions.InvalidDataException;

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
        try
        {
            var articleSavingService = new ArticleSavingService(_mainDbContext);
            articleSavingService.SaveArticle(request.ToArticleDto());
            return CreateSuccessResult();
        }
        catch (InvalidDataException)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, ""));
        }
        catch (ObjectAlreadyExistsException)
        {
            throw new RpcException(new Status(StatusCode.AlreadyExists, ""));
        }
        catch (DatabaseException)
        {
            throw new RpcException(new Status(StatusCode.Aborted, ""));
        }
        catch (Exception)
        {
            throw new RpcException(new Status(StatusCode.Internal, ""));
        }
    }

    public override Task<Article> ReadArticle(SimpleArticle request, ServerCallContext context)
    {
        try
        {
            var articleFetchingService = new ArticleFetchingService(_mainDbContext);
            var articleDto = articleFetchingService.FetchArticle(request.Ean);
            var protobufArticle = articleDto.ToProtobufArticle();
            return Task.FromResult(protobufArticle);
        }
        catch (ObjectDoesNotExistException)
        {
            throw new RpcException(new Status(StatusCode.NotFound, ""));
        }
        catch (Exception)
        {
            throw new RpcException(new Status(StatusCode.Internal, ""));
        }
    }

    public override Task<Articles> GetAllArticles(EmptyRequest request, ServerCallContext context)
    {
        try
        {
            var articleFetchingService = new ArticleFetchingService(_mainDbContext);
            var articleResultList = articleFetchingService
                .FetchArticles()
                .Select(x => x.ToProtobufArticle())
                .ToList();

            return Task.FromResult(new Articles { Values = { articleResultList } });
        }
        catch (ObjectDoesNotExistException)
        {
            throw new RpcException(new Status(StatusCode.NotFound, ""));
        }
        catch (Exception)
        {
            throw new RpcException(new Status(StatusCode.Internal, ""));
        }
    }

    public override Task<BooleanReply> UpdateArticle(Article request, ServerCallContext context)
    {
        try
        {
            var articleSavingService = new ArticleSavingService(_mainDbContext);
            articleSavingService.UpdateArticle(request.ToArticleDto());
            return CreateSuccessResult();
        }
        catch (InvalidDataException)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, ""));
        }
        catch (ObjectDoesNotExistException)
        {
            throw new RpcException(new Status(StatusCode.NotFound, ""));
        }
        catch (DatabaseException)
        {
            throw new RpcException(new Status(StatusCode.Aborted, ""));
        }
        catch (Exception)
        {
            throw new RpcException(new Status(StatusCode.Internal, ""));
        }
    }

    public override Task<BooleanReply> DeleteArticle(SimpleArticle request, ServerCallContext context)
    {
        try
        {
            var service = new ArticleDeletionService(_mainDbContext);
            service.DeleteArticle(request.Ean);

            return CreateSuccessResult();
        }
        catch (ObjectDoesNotExistException)
        {
            throw new RpcException(new Status(StatusCode.NotFound, ""));
        }
        catch (MultipleEntriesFoundException)
        {
            throw new RpcException(new Status(StatusCode.Internal, ""));
        }
        catch (DatabaseException)
        {
            throw new RpcException(new Status(StatusCode.Aborted, ""));
        }
        catch (Exception)
        {
            throw new RpcException(new Status(StatusCode.Internal, ""));
        }
    }

    private static Task<BooleanReply> CreateSuccessResult()
    {
        return Task.FromResult(new BooleanReply { Success = true });
    }
    
}
