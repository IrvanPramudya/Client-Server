using API.Models;

namespace API.DTOs.Educations
{
    public class GetViewEducationDto
    {
        public Guid Guid { get; set; }
        public string Major { get; set; }
        public string Degree { get; set; }
        public float Gpa { get; set; }
        public Guid UniversityGuid { get; set; }

        public static implicit operator Education(GetViewEducationDto getViewEducationDto)
        {
            return new Education
            {
                Guid            = getViewEducationDto.Guid,
                Major           = getViewEducationDto.Major,
                Degree          = getViewEducationDto.Degree,
                Gpa             = getViewEducationDto.Gpa,
                UniversityGuid  = getViewEducationDto.UniversityGuid,
                ModifiedDate    = DateTime.Now
            };
        }
        public static explicit operator GetViewEducationDto(Education education)
        {
            return new GetViewEducationDto
            {
                Guid            = education.Guid,
                Major           = education.Major,
                Degree          = education.Degree,
                Gpa             = education.Gpa,
                UniversityGuid  = education.UniversityGuid
            };
        }
    }
}
