using API.DTOs.Educations;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/education")]
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
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _education.GetByGuid(guid);
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost]
        public IActionResult Create(InsertEducationDto education)
        {
            var result = _education.Create(education);
            if(result == null)
            {
                return StatusCode(500,"Error Retreiving to Database");
            }
            return Ok("Data Added");
        }
        [HttpPut]
        public IActionResult Update(GetViewEducationDto education)
        {
            var result = _education.Update(education);
            if(result == 0)
            {
                return StatusCode(500, "Error Retreiving to Database");
            }
            if(result == -1)
            {
                return StatusCode(404, "Guid Not Found");
            }
            return Ok("Data Updated");
        }
        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var result = _education.Delete(guid);
            if(result == 0)
            {
                return StatusCode(500, "Error Retreiving to Database");
            }
            if(result == -1)
            {
                return StatusCode(404, "Guid Not Found");
            }
            return Ok("Data Deleted");
        }
    }
}
