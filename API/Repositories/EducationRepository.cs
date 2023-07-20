using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class EducationRepository : ITableRepository<Education>
    {
        private readonly BookingDbContext _context;

        public EducationRepository(BookingDbContext context)
        {
            _context = context;
        }

        public Education? Create(Education entity)
        {
            try
            {
                _context.Set<Education>().Add(entity);
                _context.SaveChanges();
                return entity;
            }
            catch
            {
                return null;
            }
        }

        public bool Delete(Education entity)
        {
            try
            {
                _context.Set<Education>().Remove(entity);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public IEnumerable<Education> GetAll()
        {
            return _context.Set<Education>().ToList();
        }

        public Education? GetByGuid(Guid guid)
        {
            var data = _context.Set<Education>().Find(guid);
            _context.ChangeTracker.Clear();
            return data;
        }

        public bool Update(Education entity)
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
