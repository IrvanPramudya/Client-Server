using API.Contracts;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/education")]
    public class EducationController: ControllerBase
    {
        private readonly ITableRepository<Education> _education;

        public EducationController(ITableRepository<Education> education)
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
        public IActionResult Create(Education education)
        {
            var result = _education.Create(education);
            if(result == null)
            {
                return StatusCode(500,"Error Retreiving to Database");
            }
            return Ok("Data Added");
        }
        [HttpPut]
        public IActionResult Update(Education education)
        {
            var data = _education.GetByGuid(education.Guid);
            if(data == null)
            {
                return NotFound("Guid Not Found");
            }
            var result = _education.Update(education);
            if(!result)
            {
                return StatusCode(500, "Error Retreiving to Database");
            }
            return Ok("Data Updated");
        }
        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var data = _education.GetByGuid(guid);
            if (data == null)
            {
                return NotFound("Guid Not Found");
            }
            var result = _education.Delete(data);
            if(!result)
            {
                return StatusCode(500, "Error Retreiving to Database");
            }
            return Ok("Data Deleted");
        }
    }
}
