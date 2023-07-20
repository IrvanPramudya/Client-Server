using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class BookingRepository : ITableRepository<Booking>
    {
        private readonly BookingDbContext _context;

        public BookingRepository(BookingDbContext context)
        {
            _context = context;
        }

        public Booking? Create(Booking entity)
        {
            try
            {
                _context.Set<Booking>().Add(entity);
                _context.SaveChanges();
                return entity;
            }
            catch
            {
                return null;
            }
        }

        public bool Delete(Booking entity)
        {
            try
            {
                _context.Set<Booking>().Remove(entity);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public IEnumerable<Booking> GetAll()
        {
            return _context.Set<Booking>().ToList();
        }

        public Booking? GetByGuid(Guid guid)
        {
            var data = _context.Set<Booking>().Find(guid);
            _context.ChangeTracker.Clear();
            return data;
        }

        public bool Update(Booking entity)
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
