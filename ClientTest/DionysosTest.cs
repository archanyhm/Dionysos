using DionysosProtobuf;
using Grpc.Net.Client;
using ArticleCrudService = Dionysos.Dionysos.App.GrpcService.ArticleCrudService;

namespace ClientTest;

public class DionysosTest
{
    private readonly GrpcChannel _channel;

    public DionysosTest(GrpcChannel channel)
    {
        _channel = channel;
    }

    public string FetchArticles()
    {
        var client = new DionysosProtobuf.ArticleCrudService.ArticleCrudServiceClient(_channel);

        var articles = client.GetAllArticles(new EmptyRequest());
        return articles.ToString();
    }
    //
    // public string FetchArticle()
    // {
    //     var client = new DionysosProtobuf.ArticleCrudService.ArticleCrudServiceClient(_channel);
    //     var articles = client.ReadArticle(new SimpleArticleRequest { Ean = "0000000000001" });
    //     return articles.ToString();
    // }
    //
    // public string AddArticle()
    // {
    //     var client = new ArticleCrudService.ArticleCrudServiceClient(_channel);
    //     var articleRequest = new Article();
    //     var res = client.CreateArticle(articleRequest);
    //     return res.ToString();
    // }
}
