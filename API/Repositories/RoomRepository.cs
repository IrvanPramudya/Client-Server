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

        /*public IEnumerable<Room>? GetRoom(Guid guid)
        {
            var data = _context.Set<Room>().Add();
        }*/

        public Room? GetRoomBooking(Guid guid)
        {
            return _context.Set<Room>().Find(guid);
        }

        public bool IsNotExist(string value)
        {
            return _context.Set<Room>()
                           .SingleOrDefault(room => room.Name.Contains(value)) is null;
        }
    }
}
