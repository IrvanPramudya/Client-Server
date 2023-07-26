using API.DTOs.Accounts;
using API.Services;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/account")]
    public class AccountController: ControllerBase
    {
        private readonly AccountService _account;

        public AccountController(AccountService account)
        {
            _account = account;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _account.GetAll();
            if(result == null)
            {
                return NotFound(new ResponseHandler<IEnumerable<GetViewAccountDto>>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Is Not Found",
                    Data = null
                });
            }
            return Ok(new ResponseHandler<IEnumerable<GetViewAccountDto>>
            {
                Code        = StatusCodes.Status200OK,
                Status      = HttpStatusCode.OK.ToString(),
                Message     = "Data Success Retrieved",
                Data        = result  
            });
        }
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _account.GetByGuid(guid);
            if(result == null)
            {
                return NotFound(new ResponseHandler<GetViewAccountDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid Is Not Found",
                    Data = null
                });
            }
            return Ok(new ResponseHandler<GetViewAccountDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Success Retrieved",
                Data = result
            });
        }
        [HttpPost]
        public IActionResult Create(InsertAccountDto account)
        {
            var result = _account.Create(account);
            if(result == null)
            {
                return StatusCode(500,new ResponseHandler<GetViewAccountDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Internal Server Error",
                    Data = null
                });
            }
            return Ok(new ResponseHandler<GetViewAccountDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Success Added",
                Data = result
            });
        }
        [HttpPut]
        public IActionResult Update(GetViewAccountDto account)
        {
            var result = _account.Update(account);
            if(result == 0)
            {
                return StatusCode(500, new ResponseHandler<GetViewAccountDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Internal Server Error",
                    Data = null
                });
            }
            if(result == -1)
            {
                return StatusCode(404, new ResponseHandler<GetViewAccountDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid Is Not Found",
                    Data = null
                });
            }
            return Ok(new ResponseHandler<int>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Success Updated",
                Data = result
            });
        }
        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var result = _account.Delete(guid);
            if (result == 0)
            {
                return StatusCode(500, new ResponseHandler<GetViewAccountDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Internal Server Error",
                    Data = null
                });
            }
            if (result == -1)
            {
                return StatusCode(404, new ResponseHandler<GetViewAccountDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid Is Not Found",
                    Data = null
                });
            }
            return Ok(new ResponseHandler<int>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Success Deleted",
                Data = result
            });
        }
        [HttpPost("login")]
        public IActionResult Login(LoginAccountDto login)
        {
            var data = _account.Login(login);
            if(data == 0)
            {
                return StatusCode(404, new ResponseHandler<GetViewAccountDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Email or Password is Incorrect",
                    Data = null
                });
            }
            return Ok(new ResponseHandler<int>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Successfull Login",
                Data = data
            });
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterDto register)
        {
            var data = _account.register(register);
            if(data==0)
            {
                return StatusCode(500, new ResponseHandler<GetViewAccountDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Register Failed",
                    Data = null
                });
            }
            return Ok(new ResponseHandler<int>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Successfull Register",
                Data = data
            });
        }
    }
}
