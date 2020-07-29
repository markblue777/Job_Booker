using Job_Bookings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Job_Bookings.Services
{
    public interface IAppointmentRepo
    {
        /// <summary>
        /// Create a new appointment
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        Task<bool> AddAppointment(Appointment app);

        /// <summary>
        /// Update the appointment
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        Task<Appointment> UpdateAppointment(Appointment app);

        /// <summary>
        /// soft delete, sets the booking to cancelled
        /// </summary>
        /// <param name="appGuid"></param>
        /// <returns></returns>
        Task<bool> DeleteAppointment(Guid appointmentGuid);

        /// <summary>
        /// Get appointments for a specific user based on specific date time, if dayOnly = true then get only records for the specific day in the dateTime
        /// </summary>
        /// <param name="userGuid"></param>
        /// <param name="dateTime"></param>
        /// <param name="dayOnly"></param>
        /// <returns></returns>
        Task<List<Appointment>> GetAppointments(Guid userGuid, DateTime? dateTime = null, bool dayOnly = false);

        /// <summary>
        /// Get a specific appointment base on appointment Guid and user Guid
        /// </summary>
        /// <param name="userGuid"></param>
        /// <param name="appointmentGuid"></param>
        /// <returns></returns>
        Task<Appointment> GetAppointment(Guid userGuid, Guid appointmentGuid);
    }
}
