using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class AccountRepository : ITableRepository<Account>
    {
        private readonly BookingDbContext _context;

        public AccountRepository(BookingDbContext context)
        {
            _context = context;
        }

        public Account? Create(Account entity)
        {
            try
            {
                _context.Set<Account>().Add(entity);
                _context.SaveChanges();
                return entity;
            }
            catch
            {
                return null;
            }
        }

        public bool Delete(Account entity)
        {
            try
            {
                _context.Set<Account>().Remove(entity);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public IEnumerable<Account> GetAll()
        {
            return _context.Set<Account>().ToList();
        }

        public Account? GetByGuid(Guid guid)
        {
            var data = _context.Set<Account>().Find(guid);
            _context.ChangeTracker.Clear();
            return data;
        }

        public bool Update(Account entity)
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
