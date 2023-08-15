using API.DTOs.Employees;
using API.Services;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/employee")]
    [Authorize(Roles = "Employee,Manager,Admin")]
    [EnableCors]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _employee;

        public EmployeeController(EmployeeService employee)
        {
            _employee = employee;
        }


        [HttpGet("GetCountedAtribut")]
        public IActionResult GetCountedAtribut()
        {
            var data = _employee.CountAtribut();
            if (data == null)
            {
                return StatusCode(404, new ResponseHandler<GetCountedAtribut>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data is not Found",
                    Data = null
                });
            }
            return Ok(new ResponseHandler<IEnumerable<GetCountedAtribut>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Success Retrieved",
                Data = data
            });
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _employee.GetAll();
            if (result == null)
            {
                return NotFound(new ResponseHandler<GetViewEmployeeDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Is Not Found",
                    Data = null
                });
            }
            return Ok(new ResponseHandler<IEnumerable<GetAllEmployeeDto>>
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
            var result = _employee.GetByGuid(guid);
            if (result == null)
            {
                return NotFound(new ResponseHandler<GetAllEmployeeDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid Is Not Found",
                    Data = null
                });
            }
            return Ok(new ResponseHandler<GetAllEmployeeDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Success Founded",
                Data = result
            });
        }
        [HttpPost]
        public IActionResult Create(InsertEmployeeDto employee)
        {
            var result = _employee.Create(employee);
            if (result == null)
            {
                return StatusCode(500, new ResponseHandler<GetViewEmployeeDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Internal Server Error",
                    Data = null
                });
            }
            return Ok(new ResponseHandler<GetViewEmployeeDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Success Added",
                Data = result
            });
        }
        [HttpPut]
        public IActionResult Update(GetAllEmployeeDto employee)
        {
            var result = _employee.Update(employee);
            if (result == 0)
            {
                return StatusCode(500, new ResponseHandler<GetAllEmployeeDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Internal Server Error",
                });
            }
            if (result == -1)
            {
                return StatusCode(404, new ResponseHandler<GetAllEmployeeDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid Is Not Found",
                });
            }
            return Ok(new ResponseHandler<GetAllEmployeeDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Success Updated",
            });
        }
        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var result = _employee.Delete(guid);
            if (result == 0)
            {
                return StatusCode(500, new ResponseHandler<GetAllEmployeeDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Internal Server Error",
                });
            }
            if (result == -1)
            {
                return StatusCode(404, new ResponseHandler<GetAllEmployeeDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid Is Not Found",
                });
            }
            return Ok(new ResponseHandler<GetAllEmployeeDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Success Deleted",
            });
        }
        [HttpGet("DetailEmployee")]
        public IActionResult GetEmployeeDetail()
        {
            var data = _employee.GetEmployeeDetail();
            if (data == null)
            {
                return StatusCode(404, new ResponseHandler<EmployeeDetailDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data is not Found",
                    Data = null
                });
            }
            return Ok(new ResponseHandler<IEnumerable<EmployeeDetailDto>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Success Deleted",
                Data = data
            });
        }
        [HttpGet("GetEmployeeDetailByGuid{guid}")]
        public IActionResult GetEmployeeDetailByGuid(Guid guid)
        {
            var data = _employee.GetEmployeeDetailByGuid(guid);
            if (data == null)
            {
                return StatusCode(404, new ResponseHandler<EmployeeDetailDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data is not Found",
                });
            }
            return Ok(new ResponseHandler<EmployeeDetailDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Success Deleted",
                Data = data
            });
        }
    }
}
