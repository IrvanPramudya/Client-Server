﻿using API.Contracts;
using API.DTOs.Rooms;
using API.DTOs.Universities;
using API.Models;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace API.Services
{
    public class RoomService
    {
        private readonly IRoomRepository _repository;
        private readonly IBookingRepository _bookingrepository;
        private readonly IEmployeeRepository _employeerepository;

        public RoomService(IRoomRepository repository, IBookingRepository bookingrepository, IEmployeeRepository employeerepository)
        {
            _repository = repository;
            _bookingrepository = bookingrepository;
            _employeerepository = employeerepository;
        }

        public IEnumerable<RoomDto> GetAll()
        {
            var room = _repository.GetAll();
            if(room == null)
            {
                return Enumerable.Empty<RoomDto>();
            }
            var roomlist = new List<RoomDto>();
            foreach (var rooms in room)
            {
                roomlist.Add((RoomDto)rooms);
            }
            return roomlist;
        }
        public RoomDto? GetByGuid(Guid Guid) 
        { 
            var room = _repository.GetByGuid(Guid);
            if(room == null)
            {
                return null;
            }

            return (RoomDto?)room;
        }
        public RoomDto? Create(NewRoomDto newRoomDto)
        {
            var room = _repository.Create(newRoomDto);
            if(room == null)
            {
                return null;
            }
            return (RoomDto)room; // Room berhasil ditambahkan
        }
        public int Update(RoomDto roomDto)
        {
            var room = _repository.GetByGuid(roomDto.Guid);
            if (room == null)
            {
                return -1;
            }
            Room toupdate = roomDto;
            toupdate.CreatedDate = room.CreatedDate;
            var result = _repository.Update(toupdate);
            return result ? 1       //Room terUpdate
                            : 0;    //Room Gagal Update
        }
        public int Delete(Guid guid)
        {
            var room = _repository.GetByGuid(guid);
            if (room == null)
            {
                return -1;
            }
            var result = _repository.Delete(room);
            return result ? 1       //Unversity ter Hapus
                            : 0;    //Unversity Gagal ter Hapus
        }
        /*public RoomDto? GetFreeRoom(FreeRoomDto freeRoomDto)
        {
            var data = _bookingrepository.GetStatus(freeRoomDto.Status);
            if(data is null)
            {
                return null;
            }
            var freeroomlist = new List<RoomDto>();
            var getroom = _repository.GetByGuid(data.RoomGuid);
            *//*foreach (var room in getroom)
            {

            }
            
            return (RoomDto?)getroom;*//*
        }*/
        public IEnumerable<BookedRoomDto> GetRoom()
        {
            var today = DateTime.Now.Date;
            var result = from booking in _bookingrepository.GetAll()
                         join employee in _employeerepository.GetAll() on booking.EmployeeGuid equals employee.Guid
                         join room in _repository.GetAll() on booking.RoomGuid equals room.Guid
                         where (booking.StartDate > today)
                         select new BookedRoomDto
                         {
                             BookingGuid = booking.Guid,
                             RoomName = room.Name,
                             Status = booking.Status,
                             Floor = room.Floor,
                             BookedBy = employee.FirstName + " " + employee.LastName
                         };
            return result;
        }
        
    }
}
