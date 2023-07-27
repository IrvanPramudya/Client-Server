namespace API.DTOs.Bookings
{
    public class BookingDto
    {
        public Guid RoomGuid { get; set; }
        public string RoomName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
