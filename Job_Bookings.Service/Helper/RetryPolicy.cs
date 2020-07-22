using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;


namespace Job_Bookings.Services.Helper
{
    public interface IRetryPolicy
    {
        Task<TResult> Do<TResult>(Func<Task<TResult>> retryFunc);
    }

    public class RetryPolicy : IRetryPolicy
    {

        readonly AsyncRetryPolicy _retryPolicyAsync;
        readonly IConfiguration _config;
        readonly ILogger<RetryPolicy> _logger;

        public RetryPolicy(IConfiguration config, ILogger<RetryPolicy> logger)
        {
            _config = config;
            _logger = logger;

            _retryPolicyAsync = Policy.Handle<SqlException>()
                .WaitAndRetryAsync(
                    retryCount:int.Parse(_config["retryAmmount"]),
                    sleepDurationProvider: attempt =>TimeSpan.FromMilliseconds(int.Parse(_config["retryPause"])),
                    onRetry:(exception, attempt) => {
                        _logger.LogWarning($"Retry Count: {attempt} : Message: {exception.Message}");
                    }
                );
        }

        public async Task<TResult> Do<TResult>(Func<Task<TResult>> retryFunc)
        {
            return await _retryPolicyAsync.ExecuteAsync(retryFunc.Invoke);
        }
    }
}
