using API.Contracts;
using API.Data;
using API.DTOs.AccountRoles;
using API.DTOs.Accounts;
using API.DTOs.Educations;
using API.DTOs.Employees;
using API.DTOs.Universities;
using API.Models;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace API.Services
{
    public class AccountService
    {
        private readonly IAccountRepository _repository;
        private readonly IEmployeeRepository _employeerepository;
        private readonly IEducationRepository _educationrepository;
        private readonly IUniversityRepository _universityrepository;
        private readonly BookingDbContext _dbContext;
        private readonly IEmailHandler _emailHandler;
        private readonly ITokenHandler _tokenHandler;
        private readonly IAccountRoleRepository _accountrolerepository;

        public AccountService(IAccountRepository repository,
                              IEmployeeRepository employeerepository,
                              IUniversityRepository universityrepository,
                              IEducationRepository educationrepository,
                              BookingDbContext dbContext,
                              IEmailHandler emailHandler,
                              ITokenHandler tokenHandler,
                              IAccountRoleRepository accountrolerepository)
        {
            _repository = repository;
            _employeerepository = employeerepository;
            _universityrepository = universityrepository;
            _educationrepository = educationrepository;
            _dbContext = dbContext;
            _emailHandler = emailHandler;
            _tokenHandler = tokenHandler;
            _accountrolerepository = accountrolerepository;
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

        public string Login(LoginAccountDto login)
        {
            var result = from employee in _employeerepository.GetAll()
                         join account in _repository.GetAll() on employee.Guid equals account.Guid
                         where employee.Email == login.Email && 
                         HashingHandler.ValidateHash(login.Password, account.Password)
                         select new LoginAccountDto
                         {
                             Email = employee.Email,
                             Password = account.Password
                         };
            if(!result.Any())
            {
                return "0";
            }
            var employees = _employeerepository.GetEmail(login.Email);
            var getroles = _accountrolerepository.GetRolesNameByAccountGuid(employees.Guid);
            var claims = new List<Claim>
            {
                new Claim("Guid",employees.Guid.ToString()),
                new Claim("FullName",employees.FirstName + " " + employees.LastName),
                new Claim("Email",employees.Email)
            };
            foreach(var role in getroles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var generatortoken = _tokenHandler.GenerateToken(claims);
            if(generatortoken == null)
            {
                return "-1";
            }
            return generatortoken;
        }
        public int register(RegisterDto register)
        {
            if(!_employeerepository.IsNotExist(register.Email)
                ||!_employeerepository.IsNotExist(register.PhoneNumber))
            {
                return 0;
            }
           using var transaction = _dbContext.Database.BeginTransaction();
           try
            {
                var getuniversitycode = _universityrepository.GetByCode(register.UniversityCode);
                if (getuniversitycode is null)
                {
                    var createuniversity = _universityrepository.Create(new NewUniversityDto
                    {
                        Code = register.UniversityCode,
                        Name = register.UniversityName,
                    });
                    getuniversitycode = createuniversity;
                }
                var NewNik = GenerateHandler.LastNik(_employeerepository.GetLastNik());
                var employee = _employeerepository.Create(new Employee
                {
                    Guid = new Guid(),
                    Nik = NewNik,
                    FirstName = register.FirstName,
                    LastName = register.LastName,
                    Email = register.Email,
                    PhoneNumber = register.PhoneNumber,
                    BirthDate = register.BirthDate,
                    HiringDate = register.HiringDate,
                    Gender = register.Gender,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                });
                var education = _educationrepository.Create(new InsertEducationDto
                {
                    Guid = employee.Guid,
                    Degree = register.Degree,
                    Major = register.Major,
                    Gpa = register.GPA,
                    UniversityGuid = getuniversitycode.Guid,
                });
                var account = _repository.Create(new Account
                {
                    Guid = employee.Guid,
                    Password = HashingHandler.GenerateHash(register.Password),
                    Otp = new Random().Next(111111,999999),
                    IsUsed = true,
                    ExpiredTime = DateTime.Now.AddMinutes(5),
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                });
                var accountrole = _accountrolerepository.Create(new InsertAccountRoleDto
                {
                    AccountGuid = account.Guid,
                    RoleGuid = Guid.Parse("ae259a90-e2e8-442f-ce18-08db91a71ab9")
                });
                transaction.Commit();
                return 1;
            }
            catch
            {
                transaction.Rollback();
                return -1;
            }
            
        }
        public int ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            var account = (from employee in _employeerepository.GetAll()
                                    join accounts in _repository.GetAll() on employee.Guid equals accounts.Guid
                                    where employee.Email == forgotPasswordDto.Email
                                    select accounts).FirstOrDefault();
            _repository.Clear();
            if(account == null)
            {
                return 0;
            }
            var otp = new Random().Next(111111, 999999);
            var newaccount = new Account
            {
                Guid = account.Guid,
                Password = account.Password,
                ExpiredTime = DateTime.Now.AddMinutes(5),
                Otp = otp,
                IsUsed = false,
                CreatedDate = account.CreatedDate,
                ModifiedDate = DateTime.Now
            };
            var updated = _repository.Update(newaccount);
            if(!updated)
            {
                return -1;
            }
            _emailHandler.SendEmail(forgotPasswordDto.Email, "Forgot Password", "Your OTP Code is " + otp);
            return 1;
        }
        public int ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var emailcheck = _employeerepository.CheckEmail(changePasswordDto.Email);
            if(emailcheck is null)
            {
                return 0;
            }
            var account = _repository.GetByGuid(emailcheck.Guid);
            var newaccount = new Account
            {
                Guid = account.Guid,
                IsUsed = true,
                Password = HashingHandler.GenerateHash(changePasswordDto.NewPassword),
                Otp = account.Otp,
                ExpiredTime = account.ExpiredTime,
                CreatedDate = account.CreatedDate,
                ModifiedDate = DateTime.Now
            };
            if(account.Otp != changePasswordDto.Otp)
            {
                return -1;
            }
            if(account.IsUsed)
            {
                return -2;
            }
            if(account.ExpiredTime<DateTime.Now)
            {
                return -3;
            }
            var update = _repository.Update(newaccount);
            if(!update)
            {
                return -4;
            }
            return 1;
        }

    }
}
