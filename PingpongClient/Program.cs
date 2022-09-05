// See https://aka.ms/new-console-template for more information

using Grpc.Core;
using Grpc.Net.Client;
using PingpongClient;

using var channel = GrpcChannel.ForAddress("http://localhost:5001", new GrpcChannelOptions
{
    Credentials = ChannelCredentials.Insecure
});

var testclass = new DionysosTest(channel);
var replyString = testclass.FetchArticle();

Console.WriteLine(replyString);
