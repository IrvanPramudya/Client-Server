using API.DTOs.Accounts;
using API.DTOs.Universities;
using API.Services;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/universities")]
    [Authorize(Roles = "Manager,Admin")]
    public class UniversityController:ControllerBase
    {
        private readonly UniversityService _service;

        public UniversityController(UniversityService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _service.GetAll();
            if (!result.Any())
            {
                return NotFound(new ResponseHandler<GetViewAccountDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid Is Not Found",
                    Data = null
                });
            }
            return Ok(new ResponseHandler<IEnumerable<UniversityDto>>
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
            var result = _service.GetByGuid(guid);
            if (result is null)
            {
                return NotFound(new ResponseHandler<GetViewAccountDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid Is Not Found",
                    Data = null
                });
            }
            return Ok(new ResponseHandler<UniversityDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Success Founded",
                Data = result
            });
        }
        /*public IActionResult GetByName(string name)
        {
            var result = _service.GetByName(name);
            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }*/
        [HttpPost]
        public IActionResult Create(NewUniversityDto newUniversityDto)
        {
            var result = _service.Create(newUniversityDto);
            if(result is null)
            {
                return StatusCode(500, new ResponseHandler<GetViewAccountDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Internal Server Error",
                    Data = null
                });
            }
            return Ok(new ResponseHandler<UniversityDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Success Added",
                Data = result
            });
        }
        [HttpPut]
        public IActionResult Update(UniversityDto universityDto)
        {
            var result = _service.Update(universityDto);
            if(result == 0)
            {
                return StatusCode(500, new ResponseHandler<GetViewAccountDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Internal Server Error",
                    Data = null
                });
            }
            if(result == -1)
            {
                return NotFound(new ResponseHandler<GetViewAccountDto>
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
            var result = _service.Delete(guid);
            if (result == 0)
            {
                return StatusCode(500, new ResponseHandler<GetViewAccountDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Internal Server Error",
                    Data = null
                });
            }
            if (result == -1)
            {
                return NotFound(new ResponseHandler<GetViewAccountDto>
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
