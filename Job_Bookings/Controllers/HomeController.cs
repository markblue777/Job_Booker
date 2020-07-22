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

            var res = await _custService.ListCustomers(Guid.Parse("4BF67F70-6D19-4169-AD1A-B9EFFD2478F2"));

            var resb = await _custService.GetCustomer(Guid.Parse("4BF67F70-6D19-4169-AD1A-B9EFFD2478F2"), Guid.Parse("32685BB2-CC53-4070-9DC0-1EC3113D5BC7"));

            var app = new Appointment();
            app.CustomerGuid = new Guid("32685BB2-CC53-4070-9DC0-1EC3113D5BC7");
            app.UserGuid = new Guid("4BF67F70-6D19-4169-AD1A-B9EFFD2478F2");
            app.PaymentTypeGuid = new Guid("07B08701-C60A-4DE8-B5B5-159F1DADD3BB");
            app.RateGuid = new Guid("92CF37F5-F5DD-4EBF-BCEC-6B485C98612F");
            app.AppointmentDateTime = new DateTime(2020, 7, 24, 10,0,0);
            app.AppointmentLength = 2;
            app.Notes = "test app add via controller";
            app.MaterialCosts = 22.23M;
            app.AdditionalCosts = 34M;

            var resApp = await _appointmentService.AddAppointment(app);

            
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
