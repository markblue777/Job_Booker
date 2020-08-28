using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Job_Bookings.Models;
using Job_Bookings.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Job_Bookings.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //?Add Auth on this class to privatise the API
    public class UserController : ControllerBase
    {
        readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// This retrieves a user.  It is a private API that is not exposed for general use
        /// </summary>
        /// <param name="userGuid"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get([FromQuery] Guid userGuid)
        {
            return Ok("User Get");
        }

        /// <summary>
        /// This creates a new user. It is a private API that is not exposed for general use
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody] User user) {
            return Ok("User Post");
        }

        /// <summary>
        /// This updates a User. It is a private API that is not exposed for general use
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Put([FromBody] User user) {
            return Ok("User Update");
        }
        
        /// <summary>
        /// Deletes a customer, This is a soft delete of the user and it anonymises user data for compliance to GDPR. It is a private API that is not exposed for general use
        /// </summary>
        [HttpDelete]
        public IActionResult Delete([FromQuery] Guid userGuid) {
            return Ok("User Delete");
        }
    }
}
