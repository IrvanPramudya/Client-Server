using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class EmployeeRepository : ITableRepository<Employee>
    {
        private readonly BookingDbContext _context;

        public EmployeeRepository(BookingDbContext context)
        {
            _context = context;
        }

        public Employee? Create(Employee entity)
        {
            try
            {
                _context.Set<Employee>().Add(entity);
                _context.SaveChanges();
                return entity;
            }
            catch
            {
                return null;
            }
        }

        public bool Delete(Employee entity)
        {
            try
            {
                _context.Set<Employee>().Remove(entity);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public IEnumerable<Employee> GetAll()
        {
            return _context.Set<Employee>().ToList();
        }

        public Employee? GetByGuid(Guid guid)
        {
            var data = _context.Set<Employee>().Find(guid);
            _context.ChangeTracker.Clear();
            return data;
        }

        public bool Update(Employee entity)
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
