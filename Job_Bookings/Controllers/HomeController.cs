using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Job_Bookings.Models;
using Job_Bookings.Services;

namespace Job_Bookings.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        ICustomerService _custService;
        IAppointmentService _appointmentService;

        public HomeController(ILogger<HomeController> logger, ICustomerService custService, IAppointmentService appointmentService)
        {
            _logger = logger;
            _custService = custService;
            _appointmentService = appointmentService;
        }


        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("entered index");

            return View();
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
