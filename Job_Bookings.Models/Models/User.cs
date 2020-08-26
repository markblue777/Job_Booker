using System;
using System.Collections.Generic;
using System.Text;

namespace Job_Bookings.Models
{
    public class User : BaseModel
    {
        public User()
        {
            UserGuid = Guid.NewGuid();
            Archived = false;
        }

        public Guid UserGuid { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string AddressLine1 { get; set; }
        
        public string City { get; set; }
        
        public string County { get; set; }
        public string Postcode { get; set; }
        public string CompanyName { get; set; }
        public string Timezone { get; set; }
        public string FirstName{ get; set; }
        public string LastName { get; set; }
        public bool Archived { get; set; }
    }
}
