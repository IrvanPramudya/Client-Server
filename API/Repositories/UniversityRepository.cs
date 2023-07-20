using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class UniversityRepository : ITableRepository<University>
    {
        private readonly BookingDbContext _context;

        public UniversityRepository(BookingDbContext context)
        {
            _context = context;
        }

        public University Create(University entity)
        {
            try
            {
                _context.Set<University>()
                        .Add(entity);
                _context.SaveChanges();

                return entity;
            }
            catch
            {
                return null;
            }
        }

        public bool Delete(University entity)
        {
            try
            {
                _context.Set<University>()
                        .Remove(entity);
                _context.SaveChanges() ;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<University> GetAll()
        {
            return _context.Set<University>()
                    .ToList();
        }

        public University? GetByGuid(Guid guid)
        {
            var data = _context.Set<University>()
                               .Find(guid);
            _context.ChangeTracker.Clear();
            return data;
        }

        public bool Update(University entity)
        {

            try
            {
                _context.Entry(entity)
                        .State = EntityState.Modified;
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
