using API.Contracts;
using API.DTOs.Employees;
using API.Models;

namespace API.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }
        public IEnumerable<GetViewEmployeeDto> GetAll()
        {
            var data = _repository.GetAll();
            if(data == null)
            {
                return Enumerable.Empty<GetViewEmployeeDto>();
            }
            var bookinglist = new List<GetViewEmployeeDto>();
            foreach (var booking in data)
            {
                bookinglist.Add((GetViewEmployeeDto)booking);
            }
            return bookinglist;
        }
        public GetViewEmployeeDto? GetByGuid(Guid guid)
        {
            var data = _repository.GetByGuid(guid);
            if(data == null)
            {
                return null;
            }
            return (GetViewEmployeeDto)data;
        }
        public InsertEmployeeDto Create(InsertEmployeeDto dto)
        {
            var data = _repository.Create(dto);
            if( data == null)
            {
                return null;
            }
            return (InsertEmployeeDto)data;
        }
        public int Update(GetViewEmployeeDto dto)
        {
            var data = _repository.GetByGuid(dto.Guid);
            if(data == null)
            {
                return -1;
            }
            Employee toupdate = dto;
            toupdate.CreatedDate = data.CreatedDate;
            var result = _repository.Update(dto);
            return result ? 1 : 0;
        }
        public int Delete(Guid guid)
        {
            var data = _repository.GetByGuid(guid);
            if(data == null)
            {
                return -1;
            }
            var result = _repository.Delete(data);
            return result ? 1 : 0;
        }
    }
}
