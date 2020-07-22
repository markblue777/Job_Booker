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

        public HomeController(ILogger<HomeController> logger, ICustomerService custService)
        {
            _logger = logger;
            _custService = custService;
        }

        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("entered index");

            var res = await _custService.ListCustomers(Guid.Parse("4BF67F70-6D19-4169-AD1A-B9EFFD2478F2"));

            var resb = await _custService.GetCustomer(Guid.Parse("4BF67F70-6D19-4169-AD1A-B9EFFD2478F2"), Guid.Parse("32685BB2-CC53-4070-9DC0-1EC3113D5BC7"));

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
