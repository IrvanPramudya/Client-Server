using API.DTOs.Educations;
using API.Services;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/education")]
    [Authorize]
    public class EducationController: ControllerBase
    {
        private readonly EducationService _education;

        public EducationController(EducationService education)
        {
            _education = education;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _education.GetAll();
            if(result == null)
            {
                return NotFound(new ResponseHandler<GetViewEducationDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Is Not Found",
                    Data = null
                });
            }
            return Ok(new ResponseHandler<IEnumerable<GetViewEducationDto>>
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
            var result = _education.GetByGuid(guid);
            if(result == null)
            {
                return NotFound(new ResponseHandler<GetViewEducationDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid Is Not Found",
                    Data = null
                });
            }
            return Ok(new ResponseHandler<GetViewEducationDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Success Found",
                Data = result
            });
        }
        [HttpPost]
        public IActionResult Create(InsertEducationDto education)
        {
            var result = _education.Create(education);
            if(result == null)
            {
                return StatusCode(500, new ResponseHandler<GetViewEducationDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Internal Server Error",
                    Data = null
                });
            }
            return Ok(new ResponseHandler<GetViewEducationDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Success Added",
                Data = result
            });
        }
        [HttpPut]
        public IActionResult Update(GetViewEducationDto education)
        {
            var result = _education.Update(education);
            if(result == 0)
            {
                return StatusCode(500, new ResponseHandler<GetViewEducationDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Internal Server Error",
                    Data = null
                });
            }
            if(result == -1)
            {
                return StatusCode(404, new ResponseHandler<GetViewEducationDto>
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
            var result = _education.Delete(guid);
            if(result == 0)
            {
                return StatusCode(500, new ResponseHandler<GetViewEducationDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Internal Server Error",
                    Data = null
                });
            }
            if(result == -1)
            {
                return StatusCode(404, new ResponseHandler<GetViewEducationDto>
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
