using API.Contracts;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/booking")]
    public class BookingController: ControllerBase
    {
        private readonly IBookingRepository _booking;

        public BookingController(IBookingRepository booking)
        {
            _booking = booking;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _booking.GetAll();
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var result = _booking.GetByGuid(guid);
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost]
        public IActionResult Create(Booking booking)
        {
            var result = _booking.Create(booking);
            if(result == null)
            {
                return StatusCode(500,"Error Retreiving to Database");
            }
            return Ok("Data Added");
        }
        [HttpPut]
        public IActionResult Update(Booking booking)
        {
            var data = _booking.GetByGuid(booking.Guid);
            if(data == null)
            {
                return NotFound("Guid Not Found");
            }
            var result = _booking.Update(booking);
            if(!result)
            {
                return StatusCode(500, "Error Retreiving to Database");
            }
            return Ok("Data Updated");
        }
        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var data = _booking.GetByGuid(guid);
            if (data == null)
            {
                return NotFound("Guid Not Found");
            }
            var result = _booking.Delete(data);
            if(!result)
            {
                return StatusCode(500, "Error Retreiving to Database");
            }
            return Ok("Data Deleted");
        }
    }
}
