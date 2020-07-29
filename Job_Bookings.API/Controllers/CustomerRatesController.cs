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
    public class CustomerRatesController : ControllerBase
    {
        readonly ICustomerRatesService _customerRatesService;

        public CustomerRatesController(ICustomerRatesService customerRatesService)
        {
            _customerRatesService = customerRatesService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Customer Rates Get");
        }
    }
}
