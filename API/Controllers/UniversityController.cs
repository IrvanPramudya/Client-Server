﻿using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/universities")]
    public class UniversityController:ControllerBase
    {
        private readonly IUniversityRepository _universityRepository;

        public UniversityController(IUniversityRepository universityRepository)
        {
            _universityRepository = universityRepository;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _universityRepository.GetAll();
            if (!result.Any())
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _universityRepository.GetByGuid(guid);
            if (result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost]
        public IActionResult Create(University university)
        {
            var result = _universityRepository.Create(university);
            if(result is null)
            {
                return StatusCode(500, "Error Rerieve to Database");
            }
            return Ok("Data Added");
        }
        [HttpPut]
        public IActionResult Update(University university)
        {
            var check = _universityRepository.GetByGuid(university.Guid);
            if(check is null)
            {
                return NotFound("Guid Not Found");
            }

            var result = _universityRepository.Update(university);
            if(!result)
            {
                return StatusCode(500, "Error Rerieve to Database");
            }
            return Ok("Update Success");
        }
        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var data = _universityRepository.GetByGuid(guid);
            if(data is null)
            {
                return NotFound();
            }

            var result = _universityRepository.Delete(data);
            if(!result)
            {
                return StatusCode(500, "Error Rerieve to Database");
            }
            return Ok("Delete Success");
        }
    }
}
