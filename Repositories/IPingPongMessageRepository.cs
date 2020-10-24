using pingpong.Models;

namespace pingpong.Repositories
{
    public interface IPingPongMessageRepository
    {
        void save(PingPongMessage pingPongMessage);
    }
}