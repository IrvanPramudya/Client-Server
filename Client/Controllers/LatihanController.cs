using Client.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Client.Controllers
{
    public class LatihanController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Manager")]
        public IActionResult Pokemon()
        {
            return View();
        }
    }
}