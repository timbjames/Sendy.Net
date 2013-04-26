using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sendy.Net
{
    
    public enum StatusCode
    {

        Unspecified = 0,
        Subscribed = 1,
        Unsubscribed = 2,
        Unconfirmed = 3,
        Bounced = 4,
        Soft_bounce = 5,
        Complained = 6,
        No_data_passed = 7,
        API_key_not_passed = 8,
        Invalid_API_key = 9,
        Email_not_passed = 10,
        List_ID_not_passed = 11,
        Email_does_not_exist_in_list = 12,
        Some_fields_are_missing = 13,
        Invalid_email_address = 14,
        Already_subscribed = 15,
        List_does_not_exist = 16

    }

}
