using API.DTOs.Roles;
using API.Services;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/role")]
    [Authorize(Roles = "Admin")]
    public class RoleController: ControllerBase
    {
        private readonly RoleService _role;

        public RoleController(RoleService role)
        {
            _role = role;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _role.GetAll();
            if(result == null)
            {
                return NotFound(new ResponseHandler<GetViewRoleDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid Is Not Found",
                    Data = null
                });
            }
            return Ok(new ResponseHandler<IEnumerable<GetViewRoleDto>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Success Retrived",
                Data = result
            });
        }
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _role.GetByGuid(guid);
            if(result == null)
            {
                return NotFound(new ResponseHandler<GetViewRoleDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid Is Not Found",
                    Data = null
                });
            }
            return Ok(new ResponseHandler<GetViewRoleDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Success Founded",
                Data = result
            });
        }
        [HttpPost]
        public IActionResult Create(InsertRoleDto role)
        {
            var result = _role.Create(role);
            if(result == null)
            {
                return StatusCode(500, new ResponseHandler<GetViewRoleDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Internal Server Error",
                    Data = null
                });
            }
            return Ok(new ResponseHandler<GetViewRoleDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Success Added",
                Data = result
            });
        }
        [HttpPut]
        public IActionResult Update(GetViewRoleDto getViewRoleDto)
        {
            var result = _role.Update(getViewRoleDto);
            if(result == 0)
            {
                return StatusCode(500, new ResponseHandler<GetViewRoleDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Internal Server Error",
                    Data = null
                });
            }
            if(result == -1)
            {
                return StatusCode(404, new ResponseHandler<GetViewRoleDto>
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
            var result = _role.Delete(guid);
            if(result == 0)
            {
                return StatusCode(500, new ResponseHandler<GetViewRoleDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Internal Server Error",
                    Data = null
                });
            }
            if(result == -1)
            {
                return StatusCode(404, new ResponseHandler<GetViewRoleDto>
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
