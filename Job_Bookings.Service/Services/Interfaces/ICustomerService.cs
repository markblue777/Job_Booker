using Job_Bookings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Job_Bookings.Services
{
    public interface ICustomerService
    {

        Task<List<Customer>> ListCustomers(Guid userGuid, bool onlyActive = true);
        Task<Customer> GetCustomer(Guid userGuid, Guid customerGuid);
        Task<bool> AddCustomer(Customer cust);
        
        Task<Customer> UpdateCustomer(Customer cust);
    }
}
