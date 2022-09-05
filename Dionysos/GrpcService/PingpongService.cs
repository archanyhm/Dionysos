using Grpc.Core;
using PingpongApi;

namespace Dionysos.GrpcService;

public class PingpongService : PingpongApi.PingpongService.PingpongServiceBase
{
    public override Task<PongReply> Ping(PingRequest request, ServerCallContext context)
    {
        return Task.FromResult(new PongReply { Message = request.Message + " Pong!" });
    }
}
