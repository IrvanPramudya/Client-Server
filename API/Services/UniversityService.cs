using API.Contracts;
using API.DTOs.Universities;
using API.Models;

namespace API.Services
{
    public class UniversityService
    {
        private readonly IUniversityRepository _repository;

        public UniversityService(IUniversityRepository repository)
        {
            _repository = repository;
        }
        public IEnumerable<UniversityDto> GetAll()
        {
            var universities = _repository.GetAll();
            if(!universities.Any())
            {
                return Enumerable.Empty<UniversityDto>();//Jika University kosong
            }
            var universityDtos = new List<UniversityDto>();
            foreach(var univer in universities)
            {
                universityDtos.Add((UniversityDto)univer);//Jika University Ditemukan Kemudian dijadikan Output
            }
            return universityDtos;
        }
        public UniversityDto? GetByGuid(Guid guid)
        {
            var university = _repository.GetByGuid(guid);
            if(university == null)
            {
                return null;//Mengembalikan nilai null jika university kosong
            }
            return (UniversityDto?)university;//Mengembalikan university
        }
        public UniversityDto? Create(NewUniversityDto newUniversity)
        {
            var university = _repository.Create(newUniversity);
            if(university == null)
            {
                return null; //University null atau tidak ditemukan
            }
            return (UniversityDto)university; // University berhasil ditambahkan
        }
        public int Update(UniversityDto universityDto)
        {
            var university = _repository.GetByGuid(universityDto.Guid);
            if(university == null)
            {
                return -1;
            }
            University toupdate = universityDto;
            toupdate.CreatedDate = university.CreatedDate;
            var result = _repository.Update(toupdate);
            return result ? 1       //University terUpdate
                            : 0;    //University Gagal Update
        }
        public int Delete(Guid guid)
        {
            var university = _repository.GetByGuid(guid);
            if (university == null)
            {
                return -1;
            }
            var result = _repository.Delete(university);
            return result?  1       //Unversity ter Hapus
                            : 0;    //Unversity Gagal ter Hapus
        }

    }
}
