using API.DTOs.AccountRoles;
using API.Services;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/accountrole")]
    public class AccountRoleController: ControllerBase
    {
        private readonly AccountRoleService _accountrole;

        public AccountRoleController(AccountRoleService accountrole)
        {
            _accountrole = accountrole;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _accountrole.GetAll();
            if(result == null)
            {
                return NotFound(new ResponseHandler<GetViewAccountRoleDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Is Not Found",
                    Data = null
                });
            }
            return Ok(new ResponseHandler<IEnumerable<GetViewAccountRoleDto>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Success Retrieved",
                Data = result
            });
        }
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _accountrole.GetByGuid(guid);
            if(result == null)
            {
                return NotFound(new ResponseHandler<GetViewAccountRoleDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid Is Not Found",
                    Data = null
                });
            }
            return Ok(new ResponseHandler<GetViewAccountRoleDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Success Found",
                Data = result
            });
        }
        [HttpPost]
        public IActionResult Create(InsertAccountRoleDto accountrole)
        {
            var result = _accountrole.Create(accountrole);
            if(result == null)
            {
                return StatusCode(500, new ResponseHandler<GetViewAccountRoleDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Internal Server Error",
                    Data = null
                });
            }
            return Ok(new ResponseHandler<GetViewAccountRoleDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Success Added",
                Data = result
            });
        }
        [HttpPut]
        public IActionResult Update(GetViewAccountRoleDto accountrole)
        {
            var result = _accountrole.Update(accountrole);
            if (result == 0)
            {
                return StatusCode(500, new ResponseHandler<GetViewAccountRoleDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Internal Server Error",
                    Data = null
                });
            }
            if (result == -1)
            {
                return StatusCode(404, new ResponseHandler<GetViewAccountRoleDto>
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
            var result = _accountrole.Delete(guid);
            if (result == 0)
            {
                return StatusCode(500, new ResponseHandler<GetViewAccountRoleDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Internal Server Error",
                    Data = null
                });
            }
            if (result == -1)
            {
                return StatusCode(404, new ResponseHandler<GetViewAccountRoleDto>
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
    }
}
