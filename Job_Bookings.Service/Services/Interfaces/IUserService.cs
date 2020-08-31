using Job_Bookings.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Job_Bookings.Services
{
    public interface IUserService
    {
        Task<ReturnDto<bool>> AddUser(User user);
        Task<ReturnDto<bool>> RemoveUser(Guid userGuid, bool cleanseUser = false);
        Task<ReturnDto<User>> UpdateUser(User user);
        Task<ReturnDto<User>> GetUser(Guid userGuid);
        Task<ReturnDto<List<User>>> GetUsers();

    }
}
