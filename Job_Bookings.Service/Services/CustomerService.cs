using Job_Bookings.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Job_Bookings.Services
{
    public class CustomerService : BaseService<CustomerService>, ICustomerService
    {
        ICustomerRepo _custRepo;        

        public CustomerService(ICustomerRepo custRepo, ILogger<CustomerService> logger)
        {
            _custRepo = custRepo;
            _logger = logger;
        }


        public async Task<bool> AddCustomer(Customer cust)
        {
            return await _custRepo.AddCustomer(cust);
        }

        public async Task<Customer> UpdateCustomer(Customer cust)
        {
            return await _custRepo.UpdateCustomer(cust);
        }

        public async Task<Customer> GetCustomer(Guid userGuid, Guid customerGuid)
        {
            return await _custRepo.GetCustomer(userGuid, customerGuid);
        }

        public async Task<List<Customer>> ListCustomers(Guid userGuid, bool onlyActive = true)
        {
            //TODO: Should deal with limiting in the repo and underlining call to db to minimise data transfer
            var res = await _custRepo.ListCustomers(userGuid);

            return onlyActive ? res?.Where(x => x.Archived == false).ToList() ?? null : res;
        }
    }
}
