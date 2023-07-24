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

        public bool IsNotExist(string value)
        {
            return _context.Set<Employee>()
                           .SingleOrDefault(employee => employee.Email.Contains(value)
                           ||employee.PhoneNumber.Contains(value)) is null;
        }

        /*Employee? IEmployeeRepository.GetLastNik()
        {
            var data = _context.Set<Employee>().Last(employee => employee.Nik);
        }*/

    }
}
