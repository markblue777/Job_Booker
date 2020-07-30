using Job_Bookings.Models.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Job_Bookings.Models
{
    public class ReturnDto<T>
    {
        public ErrorCodes ErrorCode { get; set; }
        public string Message => ErrorCode.GetDescription();
        public T ReturnObject {get;set;}
    }
}
