using Grpc.Net.Client;
using PingpongApi;

namespace PingpongClient;

public class PingpongClientTest
{
    public string PingPong(GrpcChannel channel)
    {
        var client = new PingpongService.PingpongServiceClient(channel);
        var reply = client.Ping(new PingRequest { Message = "Ping!" });
        return reply.Message;
    }
}