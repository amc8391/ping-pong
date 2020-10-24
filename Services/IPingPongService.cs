using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Threading;
using pingpong.Models;
using System;

namespace pingpong.Services
{
    public interface IPingPongService
    {
        Task Ping(IHubCallerClients clients, PingPongMessage receivedPing);
    }
}