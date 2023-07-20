using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class AccountRoleRepository : ITableRepository<AccountRole>
    {
        private readonly BookingDbContext _context;

        public AccountRoleRepository(BookingDbContext context)
        {
            _context = context;
        }

        public AccountRole? Create(AccountRole entity)
        {
            try
            {
                _context.Set<AccountRole>().Add(entity);
                _context.SaveChanges();
                return entity;
            }
            catch
            {
                return null;
            }
        }

        public bool Delete(AccountRole entity)
        {
            try
            {
                _context.Set<AccountRole>().Remove(entity);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public IEnumerable<AccountRole> GetAll()
        {
            return _context.Set<AccountRole>().ToList();
        }

        public AccountRole? GetByGuid(Guid guid)
        {
            var data = _context.Set<AccountRole>().Find(guid);
            _context.ChangeTracker.Clear();
            return data;
        }

        public bool Update(AccountRole entity)
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
