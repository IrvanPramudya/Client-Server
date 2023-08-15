using API.Contracts;
using API.Data;
using API.Models;
using API.Utilities.Enums;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class EmployeeRepository : TableRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(BookingDbContext context) : base(context)
        {
        }

        public Employee CheckEmail(string email)
        {
            return _context.Set<Employee>().FirstOrDefault(e=>e.Email == email);
        }

        public Employee? GetEmail(string email)
        {
            return _context.Set<Employee>().SingleOrDefault(e => e.Email.Contains(email));
        }


        public bool IsNotExist(string value)
        {
            return _context.Set<Employee>()
                           .SingleOrDefault(employee => employee.Email.Contains(value)
                           ||employee.PhoneNumber.Contains(value)) is null;
        }

        string? IEmployeeRepository.GetLastNik()
        {
            var data = _context.Set<Employee>().ToList().LastOrDefault()?.Nik;
            return data;
        }

    }
}
