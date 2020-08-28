using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Job_Bookings.Models;
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

        /// <summary>
        /// Get the rates for a customer
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(ReturnDto<List<Rate>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ReturnDto<List<Rate>>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get([FromQuery] Guid customerGuid)
        {
            var res = await _customerRatesService.GetCustomerRates(customerGuid);

            if (res.ErrorCode != ErrorCodes.NONE)
                return BadRequest(res);

            return Ok(res);
        }

        /// <summary>
        /// Create a new rate for a customer
        /// </summary>
        /// <param name="rate"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ReturnDto<bool>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ReturnDto<bool>), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Post([FromBody] Rate rate)
        {
            var res = await _customerRatesService.AddCustomerRate(rate);

            if (res.ErrorCode != ErrorCodes.NONE)
                return BadRequest(res);

            return Ok(res);
        }
    }
}
