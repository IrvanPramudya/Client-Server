using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class EmployeeRepository : TableRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(BookingDbContext context) : base(context)
        {
        }

        public Employee? GetEmail(string email)
        {
            return _context.Set<Employee>().SingleOrDefault(e => e.Email.Contains(email));
        }

        public Guid GetGuid()
        {
            var data = _context.Set<Employee>().FirstOrDefault().Guid;
            return data;
        }

        public bool IsNotExist(string value)
        {
            return _context.Set<Employee>()
                           .SingleOrDefault(employee => employee.Email.Contains(value)
                           ||employee.PhoneNumber.Contains(value)) is null;
        }

        public bool IsNotExist2(Guid Guid, string value)
        {
            var data = _context.Set<Employee>()
                .SingleOrDefault(
                    employee => 
                   employee.Email.Contains(value)
                && employee.Guid!=Guid
                || employee.PhoneNumber.Contains(value)
                && employee.Guid!=Guid) is null;
            return data;
        }

        string? IEmployeeRepository.GetLastNik()
        {
            var data = _context.Set<Employee>().OrderByDescending(e=>e.CreatedDate).FirstOrDefault().Nik;
            return data;
        }

    }
}
