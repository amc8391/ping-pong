using System;

namespace pingpong.Models
{
    public class PingPongMessage
    {
        public string date { get; set; }
        public int messageId { get; set; }
        public Guid sessionId { get; set; }
    }
}