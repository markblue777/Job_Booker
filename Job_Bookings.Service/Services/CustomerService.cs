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
        readonly ICustomerRepo _custRepo;        

        public CustomerService(ICustomerRepo custRepo, ILogger<CustomerService> logger)
        {
            _custRepo = custRepo;
            _logger = logger;
        }


        public async Task<ReturnDto<bool>> AddCustomer(Customer cust)
        {
            var rtn = new ReturnDto<bool>();

            if (cust == null)
            {
                rtn.ErrorCode = ErrorCodes.OBJECT_NOT_PROVIDED;
                rtn.Message = "No customer was provided";
                rtn.ReturnObject = false;

                return rtn;
            }

            try
            {
                rtn.ReturnObject = await _custRepo.AddCustomer(cust);
            } catch (Exception e) {
                rtn.ErrorCode = ErrorCodes.OTHER;
                rtn.Message = "An error occured";
                rtn.ReturnObject = false;

                _logger.LogError($"An error occured in - {typeof(CustomerService)} - Add Customer - Message: {e.Message} - C: {cust.CustomerGuid}, U: {cust.UserGuid}");
            }

            return rtn;
        }

        public async Task<ReturnDto<Customer>> UpdateCustomer(Customer cust)
        {
            var rtn = new ReturnDto<Customer>();

            if (cust == null)
            {
                rtn.ErrorCode = ErrorCodes.OBJECT_NOT_PROVIDED;
                rtn.Message = "No customer was provided";
                rtn.ReturnObject = null;

                return rtn;
            }

            try
            {
                rtn.ReturnObject = await _custRepo.UpdateCustomer(cust);
            } catch (Exception e) {
                rtn.ErrorCode = ErrorCodes.OTHER;
                rtn.Message = "An error occured";
                rtn.ReturnObject = null;

                _logger.LogError($"An error occured in - {typeof(CustomerService)} - Update Customer - Message: {e.Message} - C: {cust.CustomerGuid}, U: {cust.UserGuid}");
            }

            return rtn;
        }

        public async Task<ReturnDto<Customer>> GetCustomer(Guid userGuid, Guid customerGuid)
        {
            var rtn = new ReturnDto<Customer>();

            if (userGuid == null || userGuid == Guid.Empty || customerGuid == null || customerGuid == Guid.Empty)
            {
                rtn.ErrorCode = ErrorCodes.REFERENCE_GUIDS_NOT_PROVIDED;
                rtn.Message = "No user guid or customer guid was provided";
                rtn.ReturnObject = null;

                return rtn;
            }

            try 
            { 
                rtn.ReturnObject = await _custRepo.GetCustomer(userGuid, customerGuid);
            } catch (Exception e) {
                rtn.ErrorCode = ErrorCodes.OTHER;
                rtn.Message = "An error occured";
                rtn.ReturnObject = null;

                _logger.LogError($"An error occured in - {typeof(CustomerService)} - Get Customer - Message: {e.Message} - U: {userGuid} - C: {customerGuid}");
            }

            return rtn;
        }

        public async Task<ReturnDto<List<Customer>>> ListCustomers(Guid userGuid, bool onlyActive = true)
        {
            var rtn = new ReturnDto<List<Customer>>();

            //TODO: Should deal with limiting in the repo and underlining call to db to minimise data transfer

            if (userGuid == null)
            {
                rtn.ErrorCode = ErrorCodes.USER_GUID_NOT_PROVIDED;
                rtn.Message = "No user was provided";
                rtn.ReturnObject = null;

                return rtn;
            }

            List<Customer> rtnCustomers = null;
            try
            {
                rtnCustomers = await _custRepo.ListCustomers(userGuid);
            }
            catch (Exception e) {
                rtn.ErrorCode = ErrorCodes.OTHER;
                rtn.Message = "An error occured";
                rtn.ReturnObject = null;

                _logger.LogError($"An error occured in - {typeof(CustomerService)} - List Customers - Message: {e.Message} - U: {userGuid} - Active: {onlyActive}");
            }

            rtn.ReturnObject = onlyActive ? rtnCustomers?.Where(x => x.Archived == false).ToList() ?? null : rtnCustomers;

            return rtn;
        }
    }
}
