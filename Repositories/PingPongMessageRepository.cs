using pingpong.Utils;
using pingpong.Models;

namespace pingpong.Repositories
{
    public class PingPongMessageRepository : IPingPongMessageRepository
    {
        private ILogger _logger;
        public PingPongMessageRepository(ILogger logger) {
            _logger = logger;
        }

        public void save(PingPongMessage pingPongMessage) {
            _logger.warn("Saving not yet implemented");
        }
    }
}