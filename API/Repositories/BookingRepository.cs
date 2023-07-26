using API.Contracts;
using API.Data;
using API.Models;
using API.Utilities.Enums;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class BookingRepository : TableRepository<Booking>, IBookingRepository
    {
        public BookingRepository(BookingDbContext context) : base(context)
        {
        }

        public DateTime GetEndDate(Guid guid)
        {
            var data = _context.Set<Booking>().Where(booking => booking.RoomGuid == guid).SingleOrDefault().EndDate;
            return data;
        }

        public DateTime GetStartDate(Guid guid)
        {
            var data = _context.Set<Booking>().Where(booking => booking.RoomGuid == guid).SingleOrDefault().StartDate;
            return data;
        }

        public Booking? GetStatus(StatusLevel status)
        {
            return _context.Set<Booking>().Find(status);
        }
    }
}
