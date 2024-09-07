using Company.Repository;
using Company.Repository.Interfaces;
using Company.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MVC_ASP_Test.Models;
using System.Diagnostics;

namespace MVC_ASP_Test.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IEmployeeService _employeeService; // will be injected by;
        public HomeController(ILogger<HomeController> logger, IEmployeeService employeeService)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

       public IActionResult Index(string searchInp)
        {
            if (!string.IsNullOrEmpty(searchInp))
            {
                var Emps = _employeeService.GetEmployeesByName(searchInp);
                return View(Emps);
            }
            var employees = _employeeService.GetAll();
            return View(employees);
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
    }
}
