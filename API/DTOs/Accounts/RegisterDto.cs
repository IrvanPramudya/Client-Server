using API.Models;
using API.Utilities.Enums;

namespace API.DTOs.Accounts
{
    public class RegisterDto
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime HiringDate { get; set; }
        public GenderLevel Gender { get; set; }
        public string Degree { get; set; }
        public string Major { get; set; }
        public float GPA { get; set; }
        public Guid UniversityGuid { get; set; }
        public string UniversityName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }


        public RegisterDto(Account account, Employee employee, University university, Education education)
        {
            FirstName = employee.FirstName;
            LastName = employee.LastName;
            Email = employee.Email;
            PhoneNumber = employee.PhoneNumber;
            BirthDate = employee.BirthDate;
            HiringDate= employee.HiringDate;
            Gender = employee.Gender;
            Degree = education.Degree;
            Major = education.Major;
            GPA = education.Gpa;
            UniversityGuid = university.Guid;
            UniversityName = university.Name;
            Password = account.Password;

        }

        public static explicit operator RegisterDto((Account account,Employee employee,University university,Education education) data)
        {
            return new RegisterDto
            (
                data.account,
                data.employee,
                data.university,
                data.education
            );
        }
    }
}
