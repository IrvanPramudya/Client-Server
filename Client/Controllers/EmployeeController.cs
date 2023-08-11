using API.DTOs.Employees;
using API.Models;
using Client.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {

        private readonly IEmployeeRepository repository;

        public EmployeeController(IEmployeeRepository repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await repository.Get();
            var ListEmployee = new List<Employee>();

            if (result.Data != null)
            {
                ListEmployee = result.Data.ToList();
            }
            return View(ListEmployee);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(InsertEmployeeDto newEmploye)
        {

            var result = await repository.Post(newEmploye);
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
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await repository.Get(id);
            var ListEmployee = new GetAllEmployeeDto();

            if (result.Data != null)
            {
                ListEmployee = (GetAllEmployeeDto)result.Data;
            }
            return View((GetAllEmployeeDto)ListEmployee);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Employee employee)
        {
            var result = await repository.Put(employee.Guid, employee);

            if (result.Code == 200)
            {
                TempData["Success"] = $"Data has been Successfully Registered! - {result.Message}!";
                return RedirectToAction("Index", "Employee");
            }
            return RedirectToAction(nameof(Edit));

        }

        [HttpPost]
        public  async Task<IActionResult> Delete(Guid guid)
        {
            var result = await repository.Delete(guid);
            if(result.Code == 200)
            {
                TempData["Success"] = $"Data has been Successfully Deleted! - {result.Message}!";
            }
            else
            {
                TempData["Error"] = $"Data failed Deleted! - {result.Message}!";
            }
            return RedirectToAction(nameof(Index));
        }


    }
}