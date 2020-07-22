using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Job_Bookings.Models
{
    public class Customer : BaseModel
    {
        [JsonProperty("customerGuid")]
        public Guid CustomerGuid { get; set; }
        [JsonProperty("userGuid")]
        public Guid UserGuid{ get; set; }
        [JsonProperty("firstname")]
        public string FirstName { get; set; }
        [JsonProperty("lastname")]
        public string LastName { get; set; }
        [JsonProperty("phonenumber")]
        public string PhoneNumber { get; set; }
        [JsonProperty("mobilenumber")]
        public string MobileNumber { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("addressline1")]
        public string AddressLine1 { get; set; }
        [JsonProperty("city")]
        public string City { get; set; }
        [JsonProperty("county")] 
        public string County { get; set; }
        [JsonProperty("postcode")]
        public string PostCode { get; set; }
        [JsonProperty("milesfromhomebase")]
        public double MilesFromHomeBase{ get; set; }
        //the latest rate for the customer
        public List<Rate> Rates { get; set; }
        public bool Archived { get; set; }

        public List<SqlParameter> GenerateSQLParams()
        {
            List<SqlParameter> sqlParams = new List<SqlParameter>();


            return sqlParams;
        }

    }
}
