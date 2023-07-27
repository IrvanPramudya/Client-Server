using API.Contracts;
using API.DTOs.Accounts;
using API.DTOs.Educations;
using API.DTOs.Employees;
using API.DTOs.Universities;
using API.Models;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;

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
            if(!_employeerepository.IsNotExist(register.Email)
                ||!_employeerepository.IsNotExist(register.PhoneNumber))
            {
                return null;
            }
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
                    Otp = new Random().Next(111111,999999),
                    IsUsed = true,
                    ExpiredTime = DateTime.Now.AddDays(30)
                };
                var createaccount = _repository.Create(account);
                if( createaccount is null )
                {
                    return null;
                }
                var getuniversitycode = _universityrepository.GetByCode(register.UniversityCode);
                if(getuniversitycode is null )
                {
                    University university = new NewUniversityDto
                    {
                        Code = register.UniversityCode,
                        Name = register.UniversityName,
                    };
                    Education education = new InsertEducationDto
                    {
                        Guid = employee.Guid,
                        Degree = register.Degree,
                        Major = register.Major,
                        Gpa = register.GPA,
                        UniversityGuid = university.Guid,
                    };

                    var createuniversity = _universityrepository.Create(university);
                    if (createuniversity is null)
                    {
                        return null;
                    }
                    var createeducation = _educationrepository.Create(education);
                    if (createeducation is null)
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
                else
                {
                    University university = new University
                    {
                        Guid = getuniversitycode.Guid,
                        Code = getuniversitycode.Code,
                        Name = getuniversitycode.Name,
                        CreatedDate = DateTime.Now,
                        ModifiedDate = DateTime.Now
                    };
                    Education education = new InsertEducationDto
                    {
                        Guid = employee.Guid,
                        Degree = register.Degree,
                        Major = register.Major,
                        Gpa = register.GPA,
                        UniversityGuid = university.Guid,
                    };

                    var createuniversity = _universityrepository.Create(university);
                    if (createuniversity is null)
                    {
                        return null;
                    }
                    var createeducation = _educationrepository.Create(education);
                    if (createeducation is null)
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
                
            }
            catch
            {
                return null;
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
            var updated = _repository.Update(new Account
            {
                Guid = account.Guid,
                Password = account.Password,
                ExpiredTime = DateTime.Now.AddMinutes(5),
                Otp = otp,
                IsUsed = false,
                CreatedDate = account.CreatedDate,
                ModifiedDate = DateTime.Now
            });
            if(!updated)
            {
                return -1;
            }

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
                Password = changePasswordDto.NewPassword,
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
