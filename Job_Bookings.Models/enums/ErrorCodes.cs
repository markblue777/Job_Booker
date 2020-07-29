using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Job_Bookings.Models
{
    public enum ErrorCodes
    {
        [Description("An Object has not been provided.")]
        OBJECT_NOT_PROVIDED,
        [Description("Customer Guid not been provided.")]
        CUSTOMER_GUID_NOT_PROVIDED,
        [Description("User Guid not been provided.")]
        USER_GUID_NOT_PROVIDED,
        [Description("Appointment Guid not been provided.")]
        APPOINTMENT_GUID_NOT_PROVIDED,
        [Description("An error occured.")]
        OTHER
    }
}
