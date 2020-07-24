﻿using Job_Bookings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Job_Bookings.Services
{
    public interface IAppointmentService
    {

        Task<bool> AddAppointment(Appointment app);

        Task<Appointment> UpdateAppointment(Appointment app);

        Task<bool> DeleteAppointment(Guid appGuid);

        /// <summary>
        /// Get all appointments for a specific user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>List of appointments</returns>
        Task<List<Appointment>> GetAllAppointments(Guid userGuid);

        /// <summary>
        /// Get all appointments for a specific month and year, for a specific user. If dayOnly = true then it returns only the appointments for the specific date passed.
        /// </summary>
        /// <returns>List of appointments</returns>
        Task<List<Appointment>> GetAppointmentsByDate(DateTime dt, Guid userGuid, bool dayOnly = false);

        

    }
}
