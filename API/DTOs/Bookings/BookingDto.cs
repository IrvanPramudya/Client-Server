namespace API.DTOs.Bookings
{
    public class BookingDto
    {
        public Guid RoomGuid { get; set; }
        public string RoomName { get; set; }
        public double BookingLength { get; set; }
    }
}
