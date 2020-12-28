using Job_Bookings.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Job_Bookings.Services
{
    public interface IUserRepo
    {
        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<bool> AddUser(User user);
        
        /// <summary>
        /// Update a user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<User> UpdateUser(User user);
        
        /// <summary>
        /// Get a specific user, populating user would set the available search items (guid, email, first name and last name, phone number etc)
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<User> GetUser(Guid userId);
        
        /// <summary>
        /// Get a list of users
        /// </summary>
        /// <returns></returns>
        Task<List<User>> GetUsers();

        /// <summary>
        /// Soft delete, user is set the archived
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<bool> DeleteUser(Guid userGuid);

        Task<bool> ChangePassword(Guid userGuid, string password);


    }
}
