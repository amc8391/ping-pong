using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using pingpong.Models;
using pingpong.Utils;
using pingpong.Services;

namespace pingpong.Hubs
{
    public class PingPongHub : Hub
    {
        private IPingPongService _pingPongService;
        private ILogger _logger;

        public PingPongHub(IPingPongService pingPongService, ILogger logger) {
            _pingPongService = pingPongService;
            _logger = logger;
        }

        public async Task Ping(PingPongMessage receivedPing)
        {
            _logger.log("Incoming ping with Session ID: " + receivedPing.sessionId + " & Message ID " + receivedPing.messageId);
            await _pingPongService.Ping(Clients, receivedPing);
        }
    }
}