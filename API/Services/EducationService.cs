using API.Contracts;
using API.DTOs.Educations;
using API.Models;

namespace API.Services
{
    public class EducationService
    {
        private readonly IEducationRepository _repository;

        public EducationService(IEducationRepository repository)
        {
            _repository = repository;
        }
        public IEnumerable<GetViewEducationDto> GetAll() 
        { 
            var data = _repository.GetAll();
            if(data == null)
            {
                return Enumerable.Empty<GetViewEducationDto>();
            }
            var educationlist = new List<GetViewEducationDto>();
            foreach (var education in data)
            {
                educationlist.Add((GetViewEducationDto)education);
            }
            return educationlist;
        }
        public GetViewEducationDto? GetByGuid(Guid Guid) 
        {
            var data = _repository.GetByGuid(Guid);
            if(data == null)
            {
                return null;
            }
            return (GetViewEducationDto?)data;
        }
        public InsertEducationDto? Create(InsertEducationDto education)
        {
            var data = _repository.Create(education);
            if(data == null)
            {
                return null;
            }
            return (InsertEducationDto?)data;
        }
        public int Update(GetViewEducationDto education)
        {
            var data = _repository.GetByGuid(education.Guid);
            if(data == null)
            {
                return -1;
            }
            Education toupdate = education;
            toupdate.CreatedDate = data.CreatedDate;
            var result = _repository.Update(toupdate);
            return result ? 1 : 0;
        }
        public int Delete(Guid Guid)
        {
            var data = _repository.GetByGuid(Guid);
            if( data == null)
            {
                return -1;
            }
            var result = _repository.Delete(data);
            return result ? 1 : 0;
        }
    }
}
