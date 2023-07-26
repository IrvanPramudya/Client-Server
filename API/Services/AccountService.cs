using API.Contracts;
using API.DTOs.Accounts;
using API.Models;

namespace API.Services
{
    public class AccountService
    {
        private readonly IAccountRepository _repository;
        private readonly IEmployeeRepository _employeerepository;
        private readonly IEducationRepository _educationrepository;
        private readonly IUniversityRepository _universityrepository;

        public AccountService(IAccountRepository repository, IEmployeeRepository employeerepository, IUniversityRepository universityrepository, IEducationRepository educationrepository)
        {
            _repository = repository;
            _employeerepository = employeerepository;
            _universityrepository = universityrepository;
            _educationrepository = educationrepository;
        }
        public IEnumerable<GetViewAccountDto> GetAll()
        {
            var data = _repository.GetAll();
            if(data == null)
            {
                return Enumerable.Empty<GetViewAccountDto>();
            }
            var bookinglist = new List<GetViewAccountDto>();
            foreach (var booking in data)
            {
                bookinglist.Add((GetViewAccountDto)booking);
            }
            return bookinglist;
        }
        public GetViewAccountDto? GetByGuid(Guid guid)
        {
            var data = _repository.GetByGuid(guid);
            if(data == null)
            {
                return null;
            }
            return (GetViewAccountDto)data;
        }
        public GetViewAccountDto? Create(InsertAccountDto dto)
        {
            var data = _repository.Create(dto);
            if( data == null)
            {
                return null;
            }
            return (GetViewAccountDto?)data;
        }
        public int Update(GetViewAccountDto dto)
        {
            var data = _repository.GetByGuid(dto.Guid);
            if(data == null)
            {
                return -1;
            }
            Account toupdate = dto;
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

        public int Login(LoginAccountDto login)
        {
            var email = _employeerepository.GetEmail(login.Email);
            if(email is null)
            {
                return 0;
            }
            var password = _repository.GetByGuid(email.Guid);
            if(password.Password == login.Password)
            {
                return 1;
            }
            return 0;
        }
        public int register(RegisterDto register)
        {
           try
            {
                var account = new Account
                {
                    Password = register.Password
                };
                var employee = new Employee
                {
                    Guid = new Guid(),
                    FirstName = register.FirstName,
                    LastName = register.LastName,
                    Email = register.Email,
                    PhoneNumber = register.PhoneNumber,
                    BirthDate = register.BirthDate,
                    HiringDate = register.HiringDate,
                    Gender = register.Gender,
                };
                var university = new University
                {
                    Name = register.UniversityName
                };
                var education = new Education
                {
                    Degree = register.Degree,
                    Major = register.Major,
                    Gpa = register.GPA
                };
                employee.Account = account;
                education.University = university;
                education.Employee = employee;

                var createemployee = _employeerepository.Create(employee);
                var createuniversity = _universityrepository.Create(university);
                var createeducation = _educationrepository.Create(education);
                var createaccount = _repository.Create(account);

                return 1;
            }
            catch
            {
                return 0;
            }
            
        }
    }
}
