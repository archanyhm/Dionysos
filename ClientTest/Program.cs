// See https://aka.ms/new-console-template for more information

using ClientTest;
using Grpc.Core;
using Grpc.Net.Client;

using var channel = GrpcChannel.ForAddress("http://localhost:5001", new GrpcChannelOptions
{
    Credentials = ChannelCredentials.Insecure
});

var testclass = new DionysosTest(channel);
var replyString = testclass.FetchArticles();

Console.WriteLine(replyString);
