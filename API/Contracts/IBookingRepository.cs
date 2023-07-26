using API.Models;
using API.Utilities.Enums;

namespace API.Contracts
{
    public interface IBookingRepository : ITableRepository<Booking>
    {
        Booking? GetStatus(StatusLevel status);

        DateTime GetStartDate(Guid guid);
        DateTime GetEndDate(Guid guid);
    }
}
