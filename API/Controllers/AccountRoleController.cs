using API.Contracts;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/accountrole")]
    public class AccountRoleController: ControllerBase
    {
        private readonly ITableRepository<AccountRole> _accountrole;

        public AccountRoleController(ITableRepository<AccountRole> accountrole)
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
        public IActionResult Create(AccountRole accountrole)
        {
            var result = _accountrole.Create(accountrole);
            if(result == null)
            {
                return StatusCode(500,"Error Retreiving to Database");
            }
            return Ok("Data Added");
        }
        [HttpPut]
        public IActionResult Update(AccountRole accountrole)
        {
            var data = _accountrole.GetByGuid(accountrole.Guid);
            if(data == null)
            {
                return NotFound("Guid Not Found");
            }
            var result = _accountrole.Update(accountrole);
            if(!result)
            {
                return StatusCode(500, "Error Retreiving to Database");
            }
            return Ok("Data Updated");
        }
        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var data = _accountrole.GetByGuid(guid);
            if (data == null)
            {
                return NotFound("Guid Not Found");
            }
            var result = _accountrole.Delete(data);
            if(!result)
            {
                return StatusCode(500, "Error Retreiving to Database");
            }
            return Ok("Data Deleted");
        }
    }
}
