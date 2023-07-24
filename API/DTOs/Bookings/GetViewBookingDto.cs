using API.Models;
using API.Utilities.Enums;

namespace API.DTOs.Bookings
{
    public class GetViewBookingDto
    {
        public Guid Guid { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public StatusLevel Status { get; set; }
        public string Remarks { get; set; }
        public Guid RoomGuid { get; set;}
        public Guid EmployeeGuid { get; set;}

        public static implicit operator Booking(GetViewBookingDto dto)
        {
            return new Booking
            {
                Guid            = dto.Guid,
                StartDate       = dto.StartDate,
                EndDate         = dto.EndDate,
                Status          = dto.Status,
                Remarks         = dto.Remarks,
                RoomGuid        = dto.RoomGuid,
                EmployeeGuid    = dto.EmployeeGuid,
                ModifiedDate    = DateTime.Now
            };
        }
        public static explicit operator GetViewBookingDto(Booking booking)
        {
            return new GetViewBookingDto
            {
                Guid            = booking.Guid,
                StartDate       = booking.StartDate,
                EndDate         = booking.EndDate,
                Status          = booking.Status,
                Remarks         = booking.Remarks,
                RoomGuid        = booking.RoomGuid,
                EmployeeGuid    = booking.EmployeeGuid
            };
        }

    }
}
