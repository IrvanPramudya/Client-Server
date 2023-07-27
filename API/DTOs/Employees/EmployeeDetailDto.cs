using API.Utilities.Enums;

namespace API.DTOs.Employees
{
    public class EmployeeDetailDto
    {
        //Model Employee
        public Guid EmployeeGuid { get; set; }
        public string NIK { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public GenderLevel Gender { get; set; }
        public DateTime BirhtDate { get; set; }
        public DateTime HiringDate { get; set; }

        //Model University
        public string UniversityName { get; set; }

        //Model Education
        public string Major { get; set; }
        public string Degree { get; set; }
        public float GPA { get; set; }

        
    }
}
