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
    public class UserController : ControllerBase
    {
        readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] Guid userGuid)
        {
            return Ok("User Get");
        }
    }
}
