using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Job_Bookings.Models
{
    public abstract class BaseModel
    {
        public BaseModel()
        {
            DateUpdated = null;
        }

        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated{ get; set; }
    }
}
