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

            //TODO: change so the policy config is injected to allow more use cases
            _retryPolicyAsync = Policy.Handle<Exception>()
                .WaitAndRetryAsync(
                    retryCount:int.Parse(_config["retryAmount"]),
                    sleepDurationProvider: attempt => TimeSpan.FromMilliseconds(int.Parse(_config["retryPause"])),
                    onRetry:(response, delay, retryCount, context) => {
                        _logger.LogWarning($"Connection Failure - Attempt: {retryCount}, Due to - Message: {response.Message}");
                    }
                );
        }

        public async Task<TResult> Do<TResult>(Func<Task<TResult>> retryFunc)
        {
            try
            {
                return await _retryPolicyAsync.ExecuteAsync(retryFunc.Invoke);
            }
            catch {
                return (TResult)Convert.ChangeType(null, typeof(TResult));
            }
        }
    }
}
