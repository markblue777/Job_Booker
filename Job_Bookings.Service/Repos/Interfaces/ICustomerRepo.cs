using Job_Bookings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Job_Bookings.Services
{
    public interface ICustomerRepo
    {
        Task<List<Customer>> ListCustomers(Guid userGuid);
        Task<Customer> GetCustomer(Guid userGuid, Guid customerGuid);
        Task<bool> AddCustomer(Customer cust);
        Task<bool> RemoveCustomer(Guid userGuid, Guid customerGuid);
        Task<Customer> UpdateCustomer(Customer cust);
    }
}
