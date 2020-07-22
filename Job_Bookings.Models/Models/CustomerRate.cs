using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Job_Bookings.Models
{
    public class CustomerRate : BaseModel
    {
        public CustomerRate() : base() { }

        public int RatesId { get; set; }
        public Guid RatesGuid { get; set; }
        public Guid CustomerGuid { get; set; }
        public decimal HourlyRate { get; set; }
    }
}
