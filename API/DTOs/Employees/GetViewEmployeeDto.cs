using API.Models;
using API.Utilities.Enums;

namespace API.DTOs.Employees
{
    public class GetViewEmployeeDto
    {
        public Guid Guid { get; set; }
        public string Nik { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public GenderLevel Gender { get; set; }
        public DateTime HiringDate { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public static implicit operator Employee(GetViewEmployeeDto dto)
        {
            return new Employee
            {
                Guid            = dto.Guid,
                Nik             = dto.Nik,
                FirstName       = dto.FirstName,
                LastName        = dto.LastName,
                BirthDate       = dto.BirthDate,
                Gender          = dto.Gender,
                HiringDate      = dto.HiringDate,
                Email           = dto.Email,
                PhoneNumber     = dto.PhoneNumber,
                ModifiedDate    = DateTime.Now
            };
        }
        public static explicit operator GetViewEmployeeDto(Employee employee)
        {
            return new GetViewEmployeeDto
            {
                Guid            = employee.Guid,
                Nik             = employee.Nik,
                FirstName       = employee.FirstName,
                LastName        = employee.LastName,
                BirthDate       = employee.BirthDate,
                Gender          = employee.Gender,
                HiringDate      = employee.HiringDate,
                Email           = employee.Email,
                PhoneNumber     = employee.PhoneNumber,
            };
        }
    }
}
