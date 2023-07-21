using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class TableRepository<Table> : ITableRepository<Table> where Table : class
    {
        protected readonly BookingDbContext _context;
        public TableRepository(BookingDbContext context)
        {
            _context = context;
        }
        public Table? Create(Table entity)
        {
            try
            {
                _context.Set<Table>()
                        .Add(entity);
                _context.SaveChanges();

                return entity;
            }
            catch
            {
                return null;
            }
        }

        public bool Delete(Table entity)
        {
            try
            {
                _context.Set<Table>()
                        .Remove(entity);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Table> GetAll()
        {
            return _context.Set<Table>()
                    .ToList();
        }

        public Table? GetByGuid(Guid guid)
        {
            var data = _context.Set<Table>()
                               .Find(guid);
            _context.ChangeTracker.Clear();
            return data;
        }

        public bool Update(Table entity)
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
