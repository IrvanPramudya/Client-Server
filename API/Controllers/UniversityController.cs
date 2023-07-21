using API.Contracts;
using API.DTOs.Universities;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/universities")]
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
                return NotFound("Data Not Found");
            }
            return Ok(result);
        }
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _service.GetByGuid(guid);
            if (result is null)
            {
                return NotFound("Guid Not Found");
            }
            return Ok(result);
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
                return StatusCode(500, "Error Rerieve to Database");
            }
            return Ok("Data Added");
        }
        [HttpPut]
        public IActionResult Update(UniversityDto universityDto)
        {
            var result = _service.Update(universityDto);
            if(result == 0)
            {
                return StatusCode(500, "Error Rerieve to Database");
            }
            if(result == -1)
            {
                return NotFound("Guid Not Found");
            }
            return Ok("Update Success");
        }
        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var result = _service.Delete(guid);
            if (result == 0)
            {
                return StatusCode(500, "Error Rerieve to Database");
            }
            if (result == -1)
            {
                return NotFound("Guid Not Found");
            }
            return Ok("Delete Success");
        }
    }
}
