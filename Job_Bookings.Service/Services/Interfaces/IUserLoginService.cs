using Job_Bookings.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Job_Bookings.Services.Services.Interfaces
{
    public interface IUserLoginService
    {
        Task<ReturnDto<bool>> ValidateUser(string email, string password);

    }
}
