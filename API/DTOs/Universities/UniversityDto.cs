using API.Models;

namespace API.DTOs.Universities
{
    public class UniversityDto
    {
        public Guid Guid { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public static implicit operator University(UniversityDto universitydto)
        {
            return new University
            {
                Guid = universitydto.Guid,
                Code = universitydto.Code,
                Name = universitydto.Name,
                ModifiedDate = DateTime.Now

            };
        }

        public static explicit operator UniversityDto(University university)
        {
            return new UniversityDto
            {
                Guid = university.Guid,
                Code = university.Code,
                Name = university.Name
            };
        }
    }
}
