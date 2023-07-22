using API.Models;
using API.Utilities.Enums;

namespace API.DTOs.Bookings
{
    public class InsertBookingDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public StatusLevel Status { get; set; }
        public string Remarks { get; set; }
        public Guid RoomGuid { get; set; }
        public Guid EmployeeGuid { get; set; }

        public static implicit operator Booking(InsertBookingDto dto)
        {
            return new Booking
            {
                Guid = new Guid(),
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Status = dto.Status,
                Remarks = dto.Remarks,
                RoomGuid = dto.RoomGuid,
                EmployeeGuid = dto.EmployeeGuid,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }
        public static explicit operator InsertBookingDto(Booking booking)
        {
            return new InsertBookingDto
            {
                StartDate = booking.StartDate,
                EndDate = booking.EndDate,
                Status = booking.Status,
                Remarks = booking.Remarks,
                RoomGuid = booking.RoomGuid,
                EmployeeGuid = booking.EmployeeGuid
            };
        }

    }
}