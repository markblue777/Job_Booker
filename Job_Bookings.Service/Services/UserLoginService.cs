using Job_Bookings.Models;
using Job_Bookings.Services.Repos.Interfaces;
using Job_Bookings.Services.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Job_Bookings.Services.Services
{
    public class UserLoginService: BaseService<UserLoginService>, IUserLoginService
    {
        readonly IUserLoginRepo _userLoginRepo;

        public UserLoginService(IUserLoginRepo userloginrepo, ILogger<UserLoginService> logger) :base()
        {
            _userLoginRepo = userloginrepo;
            _logger = logger;
        }

        public async Task<ReturnDto<bool>> ValidateUser(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return new ReturnDto<bool> { ErrorCode = ErrorCodes.OBJECT_NOT_PROVIDED, ReturnObject = false };

            return new ReturnDto<bool> { ErrorCode = ErrorCodes.NONE, ReturnObject = await _userLoginRepo.ValidateUser(email, password) };
        }
    }
}
