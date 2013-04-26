using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sendy.Net
{
    public class Sendy
    {

        public StatusCode ErrorStatus { get; set; }
        public string StatusDescrption
        {
            get
            {
                switch (this.ErrorStatus)
                {
                    case StatusCode.Already_subscribed:
                        return "Email already subscribed";
                    case StatusCode.API_key_not_passed:
                        return "Missing API Key";
                    case StatusCode.Bounced:
                        return "Email Bounced";
                    case StatusCode.Complained:
                        return "Subscriber Complained";
                    case StatusCode.Email_does_not_exist_in_list:
                        return "Email does not exist in list";
                    case StatusCode.Email_not_passed:
                        return "Email not passed";
                    case StatusCode.Invalid_API_key:
                        return "Invalid API Key";
                    case StatusCode.Invalid_email_address:
                        return "Invalid email address";
                    case StatusCode.List_does_not_exist:
                        return "Subscriber list does not exist";
                    case StatusCode.List_ID_not_passed:
                        return "Subscriber list id not passed";
                    case StatusCode.No_data_passed:
                        return "No data passed";
                    case StatusCode.Soft_bounce:
                        return "Soft bounce";
                    case StatusCode.Some_fields_are_missing:
                        return "Some fields are missing";
                    case StatusCode.Subscribed:
                        return "Subscribed";
                    case StatusCode.Unconfirmed:
                        return "Unconfirmed";
                    case StatusCode.Unspecified:
                        return "Unscpecified status";
                    case StatusCode.Unsubscribed:
                        return "Unsubscribed";
                    default:
                        return "";

                }
            }
        }
    
        public string Response { get; set; }
        public string Parameters { get; set; }

         /// <summary>
        /// Maps the response status to our Enum SubscriptionStatus
        /// Returns Subscription.Unspecified if not matched.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        internal StatusCode GetSubscriptionStatus(string value)
        {

            // sendy pass through a . at the end of the status >.< -.-
            value = value.TrimEnd('.').ToLower();

            // match result to enum            
            for (var i = 0; i <= Enum.GetNames(typeof(StatusCode)).Length - 1; i ++)
            {
                if (Enum.GetName(typeof(StatusCode), i).ToLower().Replace("_", " ") == value)
                {
                    return (StatusCode)Enum.Parse(typeof(StatusCode), Enum.GetName(typeof(StatusCode), i));
                }                
            }   

            return StatusCode.Unspecified;

        }
        
    }
}
