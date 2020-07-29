using Job_Bookings.Models;
using Job_Bookings.Services.Helper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Job_Bookings.Services
{
    public class UserRepo : RepoBase<UserRepo>, IUserRepo
    {
        public UserRepo(IConfiguration config, ILogger<UserRepo> logger, IRetryPolicy retryPolicy) : base (config, logger, retryPolicy)
        {

        }

        public async Task<bool> AddUser(User user)
        {
            var sqlParams = new List<SqlParameter>();

            sqlParams.Add(new SqlParameter("json", JsonConvert.SerializeObject(user)));

            var res = await ExecuteWriterAsync("dbo.AddUser", sqlParams);

            return res;
        }

        public async Task<bool> DeleteUser(Guid userGuid)
        {
            var sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter { ParameterName = "@UserGuid", Value = userGuid.ToString() });

            var res = await ExecuteWriterAsync("dbo.DeleteUser", sqlParams);

            return res;
        }

        public async Task<User> GetUser(Guid userGuid)
        {
            var sqlParams = new List<SqlParameter>();

            sqlParams.Add(new SqlParameter { ParameterName = "@UserGuid", Value = userGuid.ToString() });

            var userRtn = await ExecuteReaderAsync<User>("dbo.GetUser", sqlParams);

            return userRtn;
        }

        public async Task<List<User>> GetUsers()
        {
            var sqlParams = new List<SqlParameter>();

            var userRtn = await ExecuteReaderAsync<List<User>>("dbo.GetUsers", sqlParams);

            return userRtn;
        }

        public async Task<User> UpdateUser(User user)
        {
            var sqlParams = new List<SqlParameter>();

            sqlParams.Add(new SqlParameter { ParameterName = "@json", Value = JsonConvert.SerializeObject(user) });

            var res = await ExecuteWriterAsync("dbo.UpdateUser", sqlParams);


            if (!res)
                _logger.LogError($"Type: {nameof(UserRepo)}, Failed to update user: {user.UserGuid} - Session Id: { _sessionId }");

            return await GetUser(user.UserGuid);
        }
    }
}
