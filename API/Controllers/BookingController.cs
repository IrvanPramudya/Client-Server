using API.DTOs.Bookings;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("/api/booking")]
    public class BookingController: ControllerBase
    {
        private readonly BookingService _booking;

        public BookingController(BookingService booking)
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
        public IActionResult Create(InsertBookingDto booking)
        {
            var result = _booking.Create(booking);
            if(result == null)
            {
                return StatusCode(500,"Error Retreiving to Database");
            }
            return Ok("Data Added");
        }
        [HttpPut]
        public IActionResult Update(GetViewBookingDto booking)
        {
            var result = _booking.Update(booking);
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
            var result = _booking.Delete(guid);
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
