using API.Contracts;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/role")]
    public class RoleController: ControllerBase
    {
        private readonly IRoleRepository _role;

        public RoleController(IRoleRepository role)
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
        public IActionResult Create(Role role)
        {
            var result = _role.Create(role);
            if(result == null)
            {
                return StatusCode(500,"Error Retreiving to Database");
            }
            return Ok("Data Added");
        }
        [HttpPut]
        public IActionResult Update(Role role)
        {
            var data = _role.GetByGuid(role.Guid);
            if(data == null)
            {
                return NotFound("Guid Not Found");
            }
            var result = _role.Update(role);
            if(!result)
            {
                return StatusCode(500, "Error Retreiving to Database");
            }
            return Ok("Data Updated");
        }
        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var data = _role.GetByGuid(guid);
            if (data == null)
            {
                return NotFound("Guid Not Found");
            }
            var result = _role.Delete(data);
            if(!result)
            {
                return StatusCode(500, "Error Retreiving to Database");
            }
            return Ok("Data Deleted");
        }
    }
}
