using System;
using System.Collections.Generic;
using System.Text;

namespace Job_Bookings.Models
{
    public class ReturnDto<T>
    {
        public ErrorCodes ErrorCode { get; set; }
        public string Message { get; set; }
        public T ReturnObject {get;set;}
    }
}
