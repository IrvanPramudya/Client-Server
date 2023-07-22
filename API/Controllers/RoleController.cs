using API.DTOs.Roles;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/role")]
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
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _role.GetByGuid(guid);
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost]
        public IActionResult Create(InsertRoleDto role)
        {
            var result = _role.Create(role);
            if(result == null)
            {
                return StatusCode(500,"Error Retreiving to Database");
            }
            return Ok("Data Added");
        }
        [HttpPut]
        public IActionResult Update(GetViewRoleDto getViewRoleDto)
        {
            var result = _role.Update(getViewRoleDto);
            if(result == 0)
            {
                return StatusCode(500, "Error Retreiving to Database");
            }
            if(result == -1)
            {
                return StatusCode(404, "Guid Not Found");
            }
            return Ok("Data Updated");
        }
        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var result = _role.Delete(guid);
            if(result == 0)
            {
                return StatusCode(500, "Error Retreiving to Database");
            }
            if(result == -1)
            {
                return StatusCode(404, "Guid Not Found");
            }
            return Ok("Data Deleted");
        }
    }
}
