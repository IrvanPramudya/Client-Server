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
        public EmployeeDetailDto? GetEmployeeDetailByGuid(Guid guid)
        {
            var employee = _repository.GetByGuid(guid);
            var education = _educationrepository.GetByGuid(employee.Guid);
            var university = _universityrepository.GetByGuid(education.UniversityGuid);
            if(employee== null || education == null || university == null)
            {
                return null;
            }
            return new EmployeeDetailDto
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

        }
        public IEnumerable<EmployeeDetailDto> GetEmployeeDetail()
        {
            var employees = _repository.GetAll();
            if(employees == null)
            {
                return Enumerable.Empty<EmployeeDetailDto>();
            }
            var listemployeedetail = new List<EmployeeDetailDto>();
            foreach(var data in employees)
            {
                var education = _educationrepository.GetByGuid(data.Guid);
                var university = _universityrepository.GetByGuid(education.UniversityGuid);

                EmployeeDetailDto employeeDetail = new EmployeeDetailDto
                {
                    EmployeeGuid = data.Guid,
                    NIK = data.Nik,
                    FullName = data.FirstName +" "+data.LastName,
                    PhoneNumber = data.PhoneNumber,
                    Email = data.Email,
                    Gender = data.Gender,
                    BirhtDate = data.BirthDate,
                    HiringDate = data.HiringDate,
                    UniversityName = university.Name,
                    Major = education.Major,
                    Degree = education.Degree,
                    GPA = education.Gpa
                };
                listemployeedetail.Add(employeeDetail);
            }
            return listemployeedetail;
        }
    }
}
