using API.Contracts;
using API.DTOs.Bookings;
using API.DTOs.Rooms;
using API.Models;

namespace API.Services
{
    public class BookingService
    {
        private readonly IBookingRepository _repository;
        private readonly IRoomRepository _roomrepository;
        private readonly IEmployeeRepository _employeerepository;

        public BookingService(IBookingRepository repository, IRoomRepository roomrepository, IEmployeeRepository employeerepository)
        {
            _repository = repository;
            _roomrepository = roomrepository;
            _employeerepository = employeerepository;
        }
        public IEnumerable<GetViewBookingDto> GetAll()
        {
            var data = _repository.GetAll();
            if(data == null)
            {
                return Enumerable.Empty<GetViewBookingDto>();
            }
            var bookinglist = new List<GetViewBookingDto>();
            foreach (var booking in data)
            {
                bookinglist.Add((GetViewBookingDto)booking);
            }
            return bookinglist;
        }
        public GetViewBookingDto? GetByGuid(Guid guid)
        {
            var data = _repository.GetByGuid(guid);
            if(data == null)
            {
                return null;
            }
            return (GetViewBookingDto)data;
        }
        public GetViewBookingDto? Create(InsertBookingDto dto)
        {
            var data = _repository.Create(dto);
            if( data == null)
            {
                return null;
            }
            return (GetViewBookingDto?)data;
        }
        public int Update(GetViewBookingDto dto)
        {
            var data = _repository.GetByGuid(dto.Guid);
            if(data == null)
            {
                return -1;
            }
            Booking toupdate = dto;
            toupdate.CreatedDate = data.CreatedDate;
            var result = _repository.Update(dto);
            return result ? 1 : 0;
        }
        public int Delete(Guid guid)
        {
            var data = _repository.GetByGuid(guid);
            if(data == null)
            {
                return -1;
            }
            var result = _repository.Delete(data);
            return result ? 1 : 0;
        }
        public IEnumerable<BookingDto?> BookingLength()
        {
            var bookinglist = new List<BookingDto>();
            var getbooking = _repository.GetAll();
            TimeSpan length = new TimeSpan();
            TimeSpan Start = new TimeSpan(09, 00, 00);
            TimeSpan End = new TimeSpan(17, 00, 00);
            TimeSpan OneDay = new TimeSpan(08, 00, 00);
            if (getbooking == null)
            {
                return null;
            }
            foreach(var bookings  in getbooking)
            {
                TimeSpan bookinglength = new TimeSpan();
                var startdate = bookings.StartDate;
                var enddate = bookings.EndDate;
                while (startdate < enddate)
                {
                    if (startdate.DayOfWeek != DayOfWeek.Sunday && startdate.DayOfWeek != DayOfWeek.Saturday)
                    {
                        if (startdate.TimeOfDay >= Start && enddate.TimeOfDay <= End)
                        {
                            if (startdate.TimeOfDay == enddate.TimeOfDay && startdate.Date < enddate.Date)
                            {
                                bookinglength += OneDay;
                            }
                            else
                            {
                                length = enddate.TimeOfDay - startdate.TimeOfDay;
                                bookinglength += length;
                            }
                        }
                        else
                        {
                            bookinglength += OneDay;
                        }
                        startdate = startdate.AddDays(1);
                    }
                    else
                    {
                        startdate = startdate.AddDays(1);
                    }
                }
                var room = _roomrepository.GetByGuid(bookings.RoomGuid);
                var bookinglengthdto = new BookingDto()
                {
                    RoomGuid = bookings.RoomGuid,
                    RoomName = room.Name,
                    BookingLength = bookinglength.TotalHours
                };
                bookinglist.Add(bookinglengthdto);
            }
            return bookinglist;
        }
        public IEnumerable<DetailBookingDto> GetDetailBooking()
        {
            var booking = _repository.GetAll();
            if(booking is null)
            {
                return Enumerable.Empty<DetailBookingDto>();
            }
            var listdetailbooking = new List<DetailBookingDto>();

            foreach(var data  in booking)
            {
                var employee = _employeerepository.GetByGuid(data.EmployeeGuid);
                if(employee is null)
                {
                    return Enumerable.Empty<DetailBookingDto>();
                }
                var room = _roomrepository.GetByGuid(data.RoomGuid);
                if(room is null)
                {
                    return Enumerable.Empty<DetailBookingDto>();
                }
                var bookingdetail = new DetailBookingDto
                {
                    BookingGuid = data.Guid,
                    BookedNIK = employee.Nik,
                    BookedBy = employee.FirstName + " " + employee.LastName,
                    RoomName = room.Name,
                    StartDate = data.StartDate,
                    EndDate = data.EndDate,
                    Status = data.Status,
                    Remarks = data.Remarks
                };
                listdetailbooking.Add(bookingdetail);
            }
            return listdetailbooking;
        }

    }
}
