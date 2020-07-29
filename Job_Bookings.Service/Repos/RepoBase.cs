using Job_Bookings.Services.Helper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Job_Bookings.Services
{
    public abstract class RepoBase<TBase> where TBase : RepoBase<TBase>
    {
        protected readonly IConfiguration _config;
        protected readonly ILogger<TBase> _logger;
        protected readonly IRetryPolicy _retryPolicy;

        private readonly int _timeout;
        private readonly string _connectionString;

        protected SqlConnection _conn { get; set; }
        protected string _sessionId { get; set; }


        public RepoBase(IConfiguration config, ILogger<TBase> logger, IRetryPolicy retryPolicy)
        {
            _config = config;
            _logger = logger;
            _retryPolicy = retryPolicy;

            _timeout = Int32.Parse(_config["db_timeout"]);
            _connectionString = _config["ConnectionString"];

            _sessionId = Guid.NewGuid().ToString();
        }

        public async Task<T> ExecuteReaderAsync<T>(string storedProcName, List<SqlParameter> paramList) 
        {
            try
            {
                //Implement retry logic
                object res = null;

                using var conn = new SqlConnection(_connectionString);

                conn.Open();

                using SqlCommand cmd = new SqlCommand(storedProcName, conn);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = _timeout;
                cmd.Parameters.AddRange(paramList.ToArray());

                var rtnVal = await _retryPolicy.Do(async () =>
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                        await conn.OpenAsync();

                    using SqlDataReader rdr = await cmd.ExecuteReaderAsync();

                    while (rdr.Read())
                    {
                        var rtnData = JsonConvert.DeserializeObject<T>(rdr[0].ToString());
                        res = rtnData;
                    }

                    return res;
                });

                return (T)Convert.ChangeType(res, typeof(T));
            } catch (SqlException ex) {
                _logger.LogError($"Reader Exception Occured - Sproc Name: {storedProcName}, Exception Message: {ex.Message} - Session Id: { _sessionId }");
                return default;
            }
        }

        public async Task<bool> ExecuteWriterAsync(string storedProcName, List<SqlParameter> paramList)
        {
            try
            {
                //Implement retry logic
                using var conn = new SqlConnection(_connectionString);

                conn.Open();

                using SqlCommand cmd = new SqlCommand(storedProcName, conn);

                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = _timeout;
                cmd.Parameters.AddRange(paramList.ToArray());

                var rtnVal = await _retryPolicy.Do(async () =>
                {
                    int recordsAffected = await cmd.ExecuteNonQueryAsync();

                    return recordsAffected > 0 ? true : false;
                });

                return rtnVal;
            } catch (SqlException ex) {
                _logger.LogError($"Writer Exception Occured - Sproc Name: {storedProcName}, Exception Message: {ex.Message} - Session Id: { _sessionId }");
                return false;
            }
        }
    }
}
