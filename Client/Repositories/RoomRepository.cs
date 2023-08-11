using API.DTOs.Employees;
using API.Models;
using Client.Contracts;

namespace Client.Repositories
{
    public class RoomRepository : TableRepository<Room, Guid>, IRoomRepository
    {
        public RoomRepository(string request = "room/") : base(request)
        {
        }
    }
}
