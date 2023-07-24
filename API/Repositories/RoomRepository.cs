using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class RoomRepository : TableRepository<Room>, IRoomRepository
    {
        public RoomRepository(BookingDbContext context) : base(context)
        {
        }

        public bool IsNotExist(string value)
        {
            return _context.Set<Room>()
                           .SingleOrDefault(room => room.Name.Contains(value)) is null;
        }
    }
}
