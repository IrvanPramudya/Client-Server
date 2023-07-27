using API.DTOs.Bookings;
using API.DTOs.Rooms;
using API.Services;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
                return NotFound(new ResponseHandler<GetViewBookingDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Is Not Found",
                    Data = null
                });
            }
            return Ok(new ResponseHandler<IEnumerable<GetViewBookingDto>>
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
            var result = _booking.GetByGuid(guid);
            if(result == null)
            {
                return NotFound(new ResponseHandler<GetViewBookingDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid Is Not Found",
                    Data = null
                });
            }
            return Ok(new ResponseHandler<GetViewBookingDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Success Found",
                Data = result
            });
        }
        [HttpPost]
        public IActionResult Create(InsertBookingDto booking)
        {
            var result = _booking.Create(booking);
            if(result == null)
            {
                return StatusCode(500,"Error Retreiving to Database");
            }
            return Ok(new ResponseHandler<GetViewBookingDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Success Added",
                Data = result
            });
        }
        [HttpPut]
        public IActionResult Update(GetViewBookingDto booking)
        {
            var result = _booking.Update(booking);
            if(result == 0)
            {
                return StatusCode(500, new ResponseHandler<GetViewBookingDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Internal Server Error",
                    Data = null
                });
            }
            if(result == -1)
            {
                return StatusCode(404, new ResponseHandler<GetViewBookingDto>
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
            var result = _booking.Delete(guid);
            if(result == 0)
            {
                return StatusCode(500, new ResponseHandler<GetViewBookingDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Internal Server Error",
                    Data = null
                });
            }
            if(result == -1)
            {
                return StatusCode(404, new ResponseHandler<GetViewBookingDto>
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
        [HttpGet("BookingLength")]
        public IActionResult BookingLength()
        {
            var data = _booking.BookingLength();
            if(data is null)
            {
                return NotFound(new ResponseHandler<GetViewBookingDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Guid Is Not Found",
                    Data = null
                });
            }
            return Ok(new ResponseHandler<IEnumerable<BookingDto>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Success Retrieved",
                Data = data
            });
        }
        [HttpGet("DetailBooking")]
        public IActionResult GetDetailBooking()
        {
            var data = _booking.GetDetailBooking();
            if(data is null)
            {
                return NotFound(new ResponseHandler<GetViewBookingDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Is Empty"
                });
            }

            return Ok(new ResponseHandler<IEnumerable<DetailBookingDto>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Success Retrieved",
                Data = data
            });

        }
    }
}
