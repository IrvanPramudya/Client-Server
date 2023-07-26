using API.Contracts;
using API.DTOs.Accounts;
using API.DTOs.Employees;
using API.Models;
using API.Utilities.Handlers;

namespace API.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }
        public IEnumerable<GetAllEmployeeDto> GetAll()
        {
            var data = _repository.GetAll();
            if(data == null)
            {
                return Enumerable.Empty<GetAllEmployeeDto>();
            }
            var employeelist = new List<GetAllEmployeeDto>();
            foreach (var employee in data)
            {
                employeelist.Add((GetAllEmployeeDto)employee);
            }
            return employeelist;
        }
        public GetAllEmployeeDto? GetByGuid(Guid guid)
        {
            var data = _repository.GetByGuid(guid);
            if(data == null)
            {
                return null;
            }
            return (GetAllEmployeeDto)data;
        }
        public GetViewEmployeeDto? Create(InsertEmployeeDto dto)
        {
            Employee tocreate = dto;
            tocreate.Nik = GenerateHandler.LastNik(_repository.GetLastNik());
            var data = _repository.Create(tocreate);
            if( data == null)
            {
                return null;
            }
            return (GetViewEmployeeDto)data;
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
            toupdate.Nik = _repository.GetByGuid(dto.Guid).Nik;
            var result = _repository.Update(toupdate);
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
