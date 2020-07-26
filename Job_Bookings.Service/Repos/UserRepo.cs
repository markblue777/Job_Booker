using Job_Bookings.Services.Helper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Job_Bookings.Services
{
    public class UserRepo : RepoBase<UserRepo>, IUserRepo
    {
        public UserRepo(IConfiguration config, ILogger<UserRepo> logger, IRetryPolicy retryPolicy) : base (config, logger, retryPolicy)
        {

        }
    }
}
