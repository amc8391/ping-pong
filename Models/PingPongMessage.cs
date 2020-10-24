using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace pingpong.Models
{
    public class PingPongMessage
    {
        public string date { get; set; }
        public int messageId { get; set; }
    }
}