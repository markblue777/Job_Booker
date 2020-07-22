using Job_Bookings.Models;
using Job_Bookings.Models.DTOs;
using Job_Bookings.Services.Helper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Job_Bookings.Services
{
    public class CustomerRepo : RepoBase, ICustomerRepo
    {

        public CustomerRepo(IConfiguration config, ILogger<RepoBase> logger, IRetryPolicy retryPolicy) : base(config, logger, retryPolicy)
        {

        }

        public async Task<List<Customer>> ListCustomers(Guid userGuid)
        {
            var custs = await ExecuteReaderAsync<CustomersDto>("dbo.GetCustomers", new List<SqlParameter>() { new SqlParameter() { ParameterName = "@UserGuid", Value = userGuid } });

            return custs.Customers;
        }

        public async Task<Customer> GetCustomer(Guid userGuid, Guid customerGuid) {
            var sqlParams = new List<SqlParameter>() {
                new SqlParameter() { ParameterName ="@customerGuid", Value = customerGuid },
                new SqlParameter() { ParameterName ="@UserGuid", Value = userGuid }
            };

            var cust = await ExecuteReaderAsync<Customer>("dbo.GetCustomer", sqlParams);

            return cust;
        }

        public async Task<bool> AddCustomer(Customer cust)
        {
            var sqlParams = new List<SqlParameter>();

            var res = await ExecuteWriterAsync("dbo.AddCustomer", sqlParams);
            
            return res;
        }

        public async Task<bool> RemoveCustomer(Customer cust)
        {
            var sqlParams = new List<SqlParameter>();

            var res = await ExecuteWriterAsync("dbo.RemoveCustomer", sqlParams);

            return res;
        }

        public async Task<Customer> UpdateCustomer(Customer cust)
        {
            var sqlParams = new List<SqlParameter>();

            var res = await ExecuteWriterAsync("dbo.UpdateCustomer", sqlParams);

            if (!res)
                _logger.LogWarning($"Type: {nameof(CustomerRepo)}, Failed to update customer: {cust.CustomerGuid}, for user: {cust.UserGuid}");

            return await GetCustomer(cust.UserGuid, cust.CustomerGuid);            
        }
    }
}
