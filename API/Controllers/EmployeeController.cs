using API.Contracts;
using API.DTOs.Employees;
using API.Models;
using API.Repositories;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/employee")]
    public class EmployeeController: ControllerBase
    {
        private readonly EmployeeService _employee;

        public EmployeeController(EmployeeService employee)
        {
            _employee = employee;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _employee.GetAll();
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _employee.GetByGuid(guid);
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost]
        public IActionResult Create(InsertEmployeeDto employee)
        {
            var result = _employee.Create(employee);
            if(result == null)
            {
                return StatusCode(500,"Error Retreiving to Database");
            }
            return Ok("Data Added");
        }
        [HttpPut]
        public IActionResult Update(GetViewEmployeeDto employee)
        {
            var result = _employee.Update(employee);
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
            var result = _employee.Delete(guid);
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
