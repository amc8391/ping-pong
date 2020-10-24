using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Threading;
using pingpong.Models;

namespace pingpong.Hubs
{
    public class PingPongHub : Hub
    {
        private int PING_PONG_INTERVAL = 1000;

        public async Task Ping(PingPongMessage receivedPing)
        {
            Thread.Sleep(PING_PONG_INTERVAL);

            PingPongMessage pongResponse = new PingPongMessage();
            pongResponse.date = System.DateTime.UtcNow.ToString();
            pongResponse.messageId = receivedPing.messageId + 1;

            await Clients.Caller.SendAsync("pong", pongResponse);
        }
    }
}