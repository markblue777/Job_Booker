using Job_Bookings.Services.Helper;
using Job_Bookings.Services.Repos.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Job_Bookings.Services.Repos
{
    public class UserLoginRepo : RepoBase<UserLoginRepo>, IUserLoginRepo
    {
        public UserLoginRepo(IConfiguration config, ILogger<UserLoginRepo> logger, IRetryPolicy retryPolicy) : base(config, logger, retryPolicy)
        {

        }

        /// <summary>
        /// Validate a user email and password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<bool> ValidateUser(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return false;

            var sqlParams = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = "@email", Value = email },
                new SqlParameter { ParameterName = "@password", Value = password}
            };

            var validateUser = await ExecuteReaderAsync<bool>("dbo.ValidateUuser", sqlParams);

            return validateUser;
        }
    }
}
