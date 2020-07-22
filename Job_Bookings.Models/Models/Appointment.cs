using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Job_Bookings.Models
{
    public class Appointment : BaseModel
    {
        public Appointment():base() {
            AppointmentGuid = Guid.NewGuid();
            BookingCancelled = false;
        }

        public Guid AppointmentGuid { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public double AppointmentLength { get; set; }
        public Guid CustomerGuid {get;set;}
        public Guid UserGuid { get; set; }
        public string Notes { get; set; }
        public decimal MaterialCosts { get; set; }
        public decimal AdditionalCosts { get; set; }
        public Guid PaymentTypeGuid { get; set; }
        public bool BookingCancelled { get; set; }
        public Guid RateGuid { get; set; }
        public decimal ExpectedTotal { get; set; }
    }
}
