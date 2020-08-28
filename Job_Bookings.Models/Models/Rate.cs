using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Job_Bookings.Models
{
    public class Rate : BaseModel
    {
        public Rate() : base() {
            RateGuid = Guid.NewGuid();
        }

        public Guid RateGuid { get; set; }
        public bool IsActive { get; set; }
        public Guid CustomerGuid { get; set; }           
        public decimal HourlyRate { get; set; }
    }
}
