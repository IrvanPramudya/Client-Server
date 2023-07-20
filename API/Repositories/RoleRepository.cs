using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class RoleRepository : ITableRepository<Role>
    {
        private readonly BookingDbContext _context;

        public RoleRepository(BookingDbContext context)
        {
            _context = context;
        }

        public Role? Create(Role entity)
        {
            try
            {
                _context.Set<Role>().Add(entity);
                _context.SaveChanges();
                return entity;
            }
            catch
            {
                return null;
            }
        }

        public bool Delete(Role entity)
        {
            try
            {
                _context.Set<Role>().Remove(entity);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public IEnumerable<Role> GetAll()
        {
            return _context.Set<Role>().ToList();
        }

        public Role? GetByGuid(Guid guid)
        {
            var data = _context.Set<Role>().Find(guid);
            _context.ChangeTracker.Clear();
            return data;
        }

        public bool Update(Role entity)
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
