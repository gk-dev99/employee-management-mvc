using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EmployeeManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
             return View();
        }

        [HttpPost]
        public IActionResult Index(Employee emp)
        {
            if (!ModelState.IsValid)
            {
                return View(emp);
            }
           

            TempData["Message"] = "Employee Saved Successfully";
            return RedirectToAction("Success");
        }
        
       
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

       

        //Added by Gurmeet 25-04-2026
        




   

        
    }


}

