using API.Models;

namespace API.DTOs.Educations
{
    public class InsertEducationDto
    {
        public Guid Guid { get; set; }
        public string Major { get; set; }
        public string Degree { get; set; }
        public float Gpa { get; set; }
        public Guid UniversityGuid { get; set; }

        public static implicit operator Education(InsertEducationDto dto)
        {
            return new Education
            {
                Guid            = dto.Guid,
                Major           = dto.Major,
                Degree          = dto.Degree,
                Gpa             = dto.Gpa,
                UniversityGuid  = dto.UniversityGuid,
                CreatedDate     = DateTime.Now,
                ModifiedDate    = DateTime.Now
            };
        }
        public static explicit operator InsertEducationDto(Education education)
        {
            return new InsertEducationDto
            {
                Guid            = education.Guid,
                Major           = education.Major,
                Degree          = education.Degree,
                Gpa             = education.Gpa,
                UniversityGuid  = education.UniversityGuid,
            };
        }
    }
}
