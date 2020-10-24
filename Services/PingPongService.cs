using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Threading;
using pingpong.Models;
using pingpong.Utils;
using pingpong.Repositories;
using System;

namespace pingpong.Services
{
    public class PingPongService : IPingPongService
    {
        private ILogger _logger;
        private IPingPongMessageRepository _repository;

        private int PING_PONG_INTERVAL = 1000;

        public PingPongService(ILogger logger, IPingPongMessageRepository repository) {
            _logger = logger;
            _repository = repository;
        }

        public async Task Ping(IHubCallerClients clients, PingPongMessage receivedPing)
        {
            _repository.save(receivedPing);

            _logger.log("Sleeping for " + PING_PONG_INTERVAL + " milliseconds");
            Thread.Sleep(PING_PONG_INTERVAL);

            PingPongMessage pongResponse = new PingPongMessage();
            pongResponse.date = System.DateTime.UtcNow.ToString();
            pongResponse.messageId = receivedPing.messageId + 1;

            if (receivedPing.sessionId == Guid.Empty) {
                pongResponse.sessionId = Guid.NewGuid();
                _logger.log("No session specified for incoming ping; new Session ID: " + pongResponse.sessionId.ToString());
            } else {
                pongResponse.sessionId = receivedPing.sessionId;
            }

            _logger.log("Sending pong with Session ID: " + pongResponse.sessionId.ToString() + " & Message ID: " + pongResponse.messageId.ToString());

            await clients.Caller.SendAsync("pong", pongResponse);
            _repository.save(pongResponse);
        }
    }
}