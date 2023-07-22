using API.DTOs.AccountRoles;
using API.Services;
using Microsoft.AspNetCore.Mvc;

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
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _accountrole.GetByGuid(guid);
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost]
        public IActionResult Create(InsertAccountRoleDto accountrole)
        {
            var result = _accountrole.Create(accountrole);
            if(result == null)
            {
                return StatusCode(500,"Error Retreiving to Database");
            }
            return Ok("Data Added");
        }
        [HttpPut]
        public IActionResult Update(GetViewAccountRoleDto accountrole)
        {
            var result = _accountrole.Update(accountrole);
            if (result == 0)
            {
                return StatusCode(500, "Error Retreiving to Database");
            }
            if (result == -1)
            {
                return StatusCode(404, "Guid Not Found");
            }
            return Ok("Data Updated");
        }
        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var result = _accountrole.Delete(guid);
            if (result == 0)
            {
                return StatusCode(500, "Error Retreiving to Database");
            }
            if (result == -1)
            {
                return StatusCode(404, "Guid Not Found");
            }
            return Ok("Data Deleted");
        }
    }
}
