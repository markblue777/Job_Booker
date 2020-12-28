using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Job_Bookings.Services.Repos.Interfaces
{
    public interface IUserLoginRepo
    {
        Task<bool> ValidateUser(string email, string password);
    }
}
