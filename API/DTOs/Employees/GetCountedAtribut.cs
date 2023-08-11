using API.Utilities.Enums;

namespace API.DTOs.Employees
{
    public class GetCountedAtribut
    {
        public GenderLevel Gender { get; set; }
        public int CountGender { get; set; }
        public string UniversityCode { get; set; }
        public int CountUniversity { get; set; }


    }
}
