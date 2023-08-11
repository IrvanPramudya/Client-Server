using API.DTOs.Rooms;
using API.Models;
using Client.Contracts;
using Client.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Client.Controllers
{
    public class RoomController : Controller
    {

        private readonly IRoomRepository repository;

        public RoomController(IRoomRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var result = await repository.Get();
            var ListRoom = new List<Room>();

            if (result.Data != null)
            {
                ListRoom = result.Data.ToList();
            }
            return View(ListRoom);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewRoomDto newRoom)
        {

            var result = await repository.Post(newRoom);
            if (result.Status == "200")
            {
                TempData["Success"] = "Data berhasil masuk";
                return RedirectToAction(nameof(Index));
            }
            else if (result.Status == "409")
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View();
            }
            return RedirectToAction(nameof(Index));

        }

    }
}