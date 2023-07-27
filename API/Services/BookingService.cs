using API.Contracts;
using API.DTOs.Bookings;
using API.DTOs.Rooms;
using API.Models;
using API.Utilities.Enums;

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
        public IEnumerable<BookingLengthDto?> BookingLength()
        {
            TimeSpan length = new TimeSpan();
            TimeSpan Start = new TimeSpan(09, 00, 00);
            TimeSpan End = new TimeSpan(17, 00, 00);
            TimeSpan OneDay = new TimeSpan(08, 00, 00);

            var listbooking = new List<BookingLengthDto>();

            var result = from booking in _repository.GetAll()
                         join room in _roomrepository.GetAll() on booking.RoomGuid equals room.Guid
                         select new BookingDto
                         {
                                 RoomGuid = booking.RoomGuid,
                                 RoomName = room.Name,
                                 StartDate = booking.StartDate,
                                 EndDate = booking.EndDate
                         };
            foreach(var book in result)
            {
                TimeSpan BookingLength = new TimeSpan();
                while (book.StartDate < book.EndDate)
                {
                    if (book.StartDate.DayOfWeek != DayOfWeek.Sunday && book.StartDate.DayOfWeek != DayOfWeek.Saturday)
                    {
                        if (book.StartDate.TimeOfDay >= Start && book.EndDate.TimeOfDay <= End)
                        {
                            if (book.StartDate.TimeOfDay == book.EndDate.TimeOfDay && book.StartDate.Date < book.EndDate.Date)
                            {
                                BookingLength += OneDay;
                            }
                            else
                            {
                                length = book.EndDate.TimeOfDay - book.StartDate.TimeOfDay;
                                BookingLength += length;
                            }
                        }
                        else
                        {
                            BookingLength += OneDay;
                        }
                        book.StartDate = book.StartDate.AddDays(1);
                    }
                    else
                    {
                        book.StartDate = book.StartDate.AddDays(1);
                    }
                }
                var bookinglength = new BookingLengthDto
                {
                    RoomGuid = book.RoomGuid,
                    RoomName = book.RoomName,
                    BookingLength = BookingLength.TotalHours
                };
                listbooking.Add(bookinglength);
            }
            return listbooking;
        }
        public IEnumerable<DetailBookingDto> GetDetailBooking()
        {
            var result = from employee in _employeerepository.GetAll()
                         join booking in _repository.GetAll() on employee.Guid equals booking.EmployeeGuid
                         join room in _roomrepository.GetAll() on booking.RoomGuid equals room.Guid
                         select new DetailBookingDto
                         {
                             BookingGuid = booking.Guid,
                             BookedNIK = employee.Nik,
                             BookedBy = employee.FirstName + " " + employee.LastName,
                             RoomName = room.Name,
                             StartDate = booking.StartDate,
                             EndDate = booking.EndDate,
                             Status = booking.Status,
                             Remarks = booking.Remarks
                         };
            return result;
        }
        public IEnumerable<RoomDto> FreeRoomsToday()
        {
            var result = from room in _roomrepository.GetAll()
                         join booking in _repository.GetAll() on  room.Guid equals booking.RoomGuid into books
                         from freeroom in books
                         where( freeroom.EndDate < DateTime.Now)
                         select new RoomDto
                         {
                             Guid = room.Guid,
                             Capacity = room.Capacity,
                             Floor = room.Floor,
                             Name = room.Name
                         };
            var roomresult = result.DistinctBy(room => room.Guid);
            return roomresult;
        }
    }
}
