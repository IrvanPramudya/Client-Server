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
        private readonly IEducationRepository _educationrepository;
        private readonly IUniversityRepository _universityrepository;

        public EmployeeService(IEmployeeRepository repository, IEducationRepository educationrepository, IUniversityRepository universityrepository)
        {
            _repository = repository;
            _educationrepository = educationrepository;
            _universityrepository = universityrepository;
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
        public int Update(GetAllEmployeeDto dto)
        {
            var data = _repository.GetByGuid(dto.Guid);
            if(data == null)
            {
                return -1;
            }
            Employee toupdate = dto;
            toupdate.CreatedDate = data.CreatedDate;
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
        public EmployeeDetailDto? GetEmployeeDetailByGuid(Guid guid)
        {
            return GetEmployeeDetail().SingleOrDefault(e=>e.EmployeeGuid.Equals(guid));

        }
        public IEnumerable<EmployeeDetailDto> GetEmployeeDetail()
        {
            var result = from employee in _repository.GetAll()
                         join education in _educationrepository.GetAll() on employee.Guid equals education.Guid
                         join university in _universityrepository.GetAll() on education.UniversityGuid equals university.Guid
                         select new EmployeeDetailDto
                         {
                             EmployeeGuid = employee.Guid,
                             NIK = employee.Nik,
                             FullName = employee.FirstName + " " + employee.LastName,
                             PhoneNumber = employee.PhoneNumber,
                             Email = employee.Email,
                             Gender = employee.Gender,
                             BirhtDate = employee.BirthDate,
                             HiringDate = employee.HiringDate,
                             UniversityName = university.Name,
                             Major = education.Major,
                             Degree = education.Degree,
                             GPA = education.Gpa
                         };
            return result;
        }
        public IEnumerable<GetCountedAtribut> CountAtribut()
        {
            var result = from employee in _repository.GetAll()
                         join education in _educationrepository.GetAll() on employee.Guid equals education.Guid
                         join university in _universityrepository.GetAll() on education.UniversityGuid equals university.Guid
                         group employee by new { employee.Gender, university.Code } into grouped
                         select new GetCountedAtribut
                         {
                             Gender = grouped.Key.Gender,
                             CountGender = grouped.Count(),
                             UniversityCode = grouped.Key.Code,
                             CountUniversity = grouped.Count(),
                         };
            return result;
        }
    }
}
