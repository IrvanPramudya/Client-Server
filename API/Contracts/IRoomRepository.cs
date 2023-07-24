using API.Models;

namespace API.Contracts
{
    public interface IRoomRepository : ITableRepository<Room>
    {
        bool IsNotExist(string value);
    }
}
