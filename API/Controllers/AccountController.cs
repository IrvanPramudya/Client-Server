using API.Contracts;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/account")]
    public class AccountController: ControllerBase
    {
        private readonly ITableRepository<Account> _account;

        public AccountController(ITableRepository<Account> account)
        {
            _account = account;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _account.GetAll();
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _account.GetByGuid(guid);
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost]
        public IActionResult Create(Account account)
        {
            var result = _account.Create(account);
            if(result == null)
            {
                return StatusCode(500,"Error Retreiving to Database");
            }
            return Ok("Data Added");
        }
        [HttpPut]
        public IActionResult Update(Account account)
        {
            var data = _account.GetByGuid(account.Guid);
            if(data == null)
            {
                return NotFound("Guid Not Found");
            }
            var result = _account.Update(account);
            if(!result)
            {
                return StatusCode(500, "Error Retreiving to Database");
            }
            return Ok("Data Updated");
        }
        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var data = _account.GetByGuid(guid);
            if (data == null)
            {
                return NotFound("Guid Not Found");
            }
            var result = _account.Delete(data);
            if(!result)
            {
                return StatusCode(500, "Error Retreiving to Database");
            }
            return Ok("Data Deleted");
        }
    }
}
