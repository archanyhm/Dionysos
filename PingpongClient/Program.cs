// See https://aka.ms/new-console-template for more information

using System.Net.NetworkInformation;
using Grpc.Core;
using Grpc.Net.Client;
using PingpongApi;

using var channel = GrpcChannel.ForAddress("http://localhost:5001", new GrpcChannelOptions
{
    Credentials = ChannelCredentials.Insecure
});
var client = new PingpongService.PingpongServiceClient(channel);
var reply = client.Ping(new PingRequest { Message = "Ping!" });

Console.WriteLine(reply.Message);