using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Job_Bookings.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Job_Bookings.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appService)
        {
            _appointmentService = appService;
        }
        
        [HttpGet]
        public IActionResult Get() {
            return Ok("Appointment Get");
        }
    }
}
