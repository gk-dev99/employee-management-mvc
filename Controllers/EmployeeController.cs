using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services;
using EmployeeManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService; 
        }


        public async Task<IActionResult> Index()
        {
            var employees = await _employeeService.GetAllAsync();
            return View(employees);

        }
        public async Task<IActionResult> Details(int id)
        {
            var emp = await _employeeService.GetbyIdAsync(id);
            if (emp == null)
                return NotFound();
            return View(emp);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View(new EmployeeVM());

        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeVM empVM)
        {
            if (!ModelState.IsValid)
                return View(empVM);

            try
            {
                var exists = await _employeeService.ExistByNameAsync(empVM.Name);

                if (exists)
                {
                    ModelState.AddModelError("Name", "Employee with this name already exists");
                    return View(empVM);
                }

                var emp = new Employee()
                {
                    Name = empVM.Name,
                    Age = empVM.Age
                };
                await _employeeService.AddAsync(emp);
                TempData["Success"] = "Employee created successfully";
                return RedirectToAction("Index");

            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Something went Wrong. Please try again..");
                return View(empVM);

            }
            



        }
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var emp = await _employeeService.GetbyIdAsync(Id);
            if (emp == null)
                return NotFound();
            var empVM = new EmployeeVM
            {
                Id = emp.Id,
                Name = emp.Name,
                Age = emp.Age
            };
            return View(empVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeVM empVM)
        {

            if (!ModelState.IsValid)
            {
                return View(empVM);
            }

            try
            {
                var existingEmp = await _employeeService.GetbyIdAsync(empVM.Id);
                if (existingEmp == null)
                {
                    return NotFound();
                }
                var emp = new Employee
                {
                    Id = empVM.Id,
                    Name = empVM.Name,
                    Age = empVM.Age
                };
                await _employeeService.UpdateAsync(emp);
                TempData["Success"] = "Employee Updated Successfully";

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Something went wrong.Please try again..");
                return View(empVM);

            }

        }

        [HttpPost]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                var emp = await _employeeService.GetbyIdAsync(Id);
                if (emp == null)
                {
                    return NotFound();
                }
                await _employeeService.DeleteAsync(Id);

                TempData["Success"] = "Employee Deleted Successfully";

                return RedirectToAction("Index");
            }

            catch (Exception)
            {
                TempData["Error"] = "Something went Wrong. Please try Again";
                return RedirectToAction("Index");

            }
            
        }
    }
}
