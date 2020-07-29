using Job_Bookings.Models;
using Job_Bookings.Services.Helper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Job_Bookings.Services
{
    public class AppointmentRepo : RepoBase<AppointmentRepo>, IAppointmentRepo
    {
        public AppointmentRepo(IConfiguration config, ILogger<AppointmentRepo> logger, IRetryPolicy retryPolicy) : base(config, logger, retryPolicy)
        {
            
        }

        public async Task<bool> AddAppointment(Appointment app)
        {
            var sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter() { ParameterName = "@json", Value = JsonConvert.SerializeObject(app) });
            var res = await ExecuteWriterAsync("dbo.AddAppointment", sqlParams);

            return res;
        }

        public async Task<bool> DeleteAppointment(Guid appointmentGuid)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            sqlParameters.Add(new SqlParameter { ParameterName = "@AppointmentGuid", Value = appointmentGuid.ToString() });

            return await ExecuteWriterAsync("dbo.DeleteAppointment", sqlParameters);
        }

        public async Task<Appointment> GetAppointment(Guid userGuid, Guid appointmentGuid)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter { ParameterName = "@UserGuid", Value = userGuid.ToString() });
            sqlParameters.Add(new SqlParameter { ParameterName = "@AppointmentGuid", Value = appointmentGuid.ToString() });


            return await ExecuteReaderAsync<Appointment>("dbo.GetAppointment", sqlParameters);
        }

        public async Task<List<Appointment>> GetAppointments(Guid userGuid, DateTime? dateTime = null, bool dayOnly = false)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter { ParameterName = "@AppointmentGuid", Value = userGuid.ToString() });
            sqlParameters.Add(new SqlParameter { ParameterName = "@DateTime", Value = dateTime ?? null });
            sqlParameters.Add(new SqlParameter { ParameterName = "@DayOnly", Value = dayOnly });


            return await ExecuteReaderAsync<List<Appointment>>("dbo.GetAppointments", sqlParameters);
        }

        public async Task<Appointment> UpdateAppointment(Appointment app)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>();

            sqlParameters.Add(new SqlParameter { ParameterName = "@json", Value = JsonConvert.SerializeObject(app) });

            var res = await ExecuteWriterAsync("dbo.UpdateAppointment", sqlParameters);

            if (!res)
                _logger.LogError($"Type: {nameof(AppointmentRepo)}, Failed to update appointment: {app.AppointmentGuid}, for user: {app.UserGuid} - Session Id: { _sessionId }");

            return await GetAppointment(app.UserGuid, app.AppointmentGuid);


        }
    }
}
