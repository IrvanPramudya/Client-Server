using API.Contracts;
using API.DTOs.Rooms;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/service")]
    public class RoomController: ControllerBase
    {
        private readonly RoomService _service;

        public RoomController(RoomService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _service.GetAll();
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _service.GetByGuid(guid);
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost]
        public IActionResult Create(NewRoomDto newRoomDto)
        {
            var result = _service.Create(newRoomDto);
            if(result == null)
            {
                return StatusCode(500,"Error Retreiving to Database");
            }
            return Ok("Data Added");
        }
        [HttpPut]
        public IActionResult Update(RoomDto roomDto)
        {
            var result = _service.Update(roomDto);
            if(result == 0)
            {
                return StatusCode(500, "Error Retreiving to Database");
            }
            if(result == -1)
            {
                return NotFound("Guid Not Found");
            }
            return Ok("Data Updated");
        }
        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var result = _service.Delete(guid);
            if (result == 0)
            {
                return StatusCode(500, "Error Retreiving to Database");
            }
            if (result == -1)
            {
                return NotFound("Guid Not Found");
            }
            return Ok("Data Deleted");

        }
    }
}
