using API.Contracts;
using API.DTOs.Accounts;
using API.DTOs.Educations;
using API.DTOs.Employees;
using API.DTOs.Universities;
using API.Models;
using API.Utilities.Handlers;

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
        public RegisterDto? register(RegisterDto register)
        {
           try
            {
                
                Employee employee = new InsertEmployeeDto
                {
                    FirstName = register.FirstName,
                    LastName = register.LastName,
                    Email = register.Email,
                    PhoneNumber = register.PhoneNumber,
                    BirthDate = register.BirthDate,
                    HiringDate = register.HiringDate,
                    Gender = register.Gender
                };
                employee.Nik = GenerateHandler.LastNik(_employeerepository.GetLastNik());
                var createemployee = _employeerepository.Create(employee);
                if(createemployee is null)
                {
                    return null;
                }
                Account account = new InsertAccountDto
                {
                    Guid = employee.Guid,
                    Password = register.Password,
                    Otp = 111,
                    IsUsed = true,
                    ExpiredTime = DateTime.Now.AddDays(30)
                };
                var createaccount = _repository.Create(account);
                if( createaccount is null )
                {
                    return null;
                }

                University university = new NewUniversityDto
                {
                    Code = register.UniversityCode,
                    Name = register.UniversityName,
                };
                var createuniversity = _universityrepository.Create(university);
                if(createuniversity is null )
                {
                    return null;
                }

                Education education = new InsertEducationDto
                {
                    Guid = employee.Guid,
                    Degree = register.Degree,
                    Major = register.Major,
                    Gpa = register.GPA,
                    UniversityGuid = university.Guid,
                };
                
                var createeducation = _educationrepository.Create(education);
                if(createeducation is null)
                {
                    return null;
                }
                

                return new RegisterDto
                {
                    FirstName = createemployee.FirstName,
                    LastName = createemployee.LastName,
                    Email = createemployee.Email,
                    PhoneNumber = createemployee.PhoneNumber,
                    BirthDate = createemployee.BirthDate,
                    HiringDate = createemployee.HiringDate,
                    Gender = createemployee.Gender,
                    Degree = createeducation.Degree,
                    Major = createeducation.Major,
                    GPA = createeducation.Gpa,
                    UniversityCode = createuniversity.Code,
                    UniversityName = createuniversity.Name,
                    Password = createaccount.Password,
                };
            }
            catch
            {
                return null;
            }
            
        }
    }
}
