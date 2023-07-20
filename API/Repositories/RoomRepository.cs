using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class RoomRepository : ITableRepository<Room>
    {
        private readonly BookingDbContext _context;

        public RoomRepository(BookingDbContext context)
        {
            _context = context;
        }

        public Room? Create(Room entity)
        {
            try
            {
                _context.Set<Room>().Add(entity);
                _context.SaveChanges();
                return entity;
            }
            catch
            {
                return null;
            }
        }

        public bool Delete(Room entity)
        {
            try
            {
                _context.Set<Room>().Remove(entity);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public IEnumerable<Room> GetAll()
        {
            return _context.Set<Room>().ToList();
        }

        public Room? GetByGuid(Guid guid)
        {
            var data = _context.Set<Room>().Find(guid);
            _context.ChangeTracker.Clear();
            return data;
        }

        public bool Update(Room entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
