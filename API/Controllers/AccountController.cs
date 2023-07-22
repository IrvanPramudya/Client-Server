using API.Contracts;
using API.DTOs.Accounts;
using API.Models;
using API.Repositories;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

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
        public IActionResult Create(InsertAccountDto account)
        {
            var result = _account.Create(account);
            if(result == null)
            {
                return StatusCode(500,"Error Retreiving to Database");
            }
            return Ok("Data Added");
        }
        [HttpPut]
        public IActionResult Update(GetViewAccountDto account)
        {
            var result = _account.Update(account);
            if(result == 0)
            {
                return StatusCode(500, "Error Retreiving to Database");
            }
            if(result == -1)
            {
                return StatusCode(500, "Error Retreiving to Database");
            }
            return Ok("Data Updated");
        }
        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var result = _account.Delete(guid);
            if (result == 0)
            {
                return StatusCode(500, "Error Retreiving to Database");
            }
            if (result == -1)
            {
                return StatusCode(500, "Error Retreiving to Database");
            }
            return Ok("Data Updated");
        }
    }
}
