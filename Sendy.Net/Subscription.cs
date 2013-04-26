using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sendy.Net
{
    
    // http://sendy.co/api

    public class Subscription : Sendy
    {
        
        public Subscription()
        {
        }

        /// <summary>
        /// Calls Sendy API to subscribe user
        /// API POST URL: http://your_sendy_installation/subscribe
        /// </summary>
        /// <param name="listId"></param>
        /// <param name="email"></param>
        /// <param name="htmlEmails"></param>
        public bool Subscribe(string listId, string email, bool plainTextResponse)
        {
            return Subscribe(listId, email, "", plainTextResponse);
        }

        /// <summary>
        /// Calls Sendy API to subscribe user
        /// API POST URL: http://your_sendy_installation/subscribe
        /// </summary>
        /// <param name="listId"></param>
        /// <param name="email"></param>
        /// <param name="name"></param>
        /// <param name="plainTextResponse"></param>
        public bool Subscribe(string listId, string email, string name, bool plainTextResponse)
        {
            
            // api url
            var result = false;
            var apiUrl = Config.AppConfig.getConfig().Sendy.InstallationUrl + "/subscribe";   
         
            // set the parameters to post
            this.Parameters = string.Format("list={0}&email={1}&boolean={2}{3}", 
                listId, 
                email, 
                plainTextResponse ? "true" : "false", 
                string.IsNullOrEmpty(name) ? "" : string.Format("&name={0}", name));

            // post info to sendy api
            this.Response = new HttpPost("application/x-www-form-urlencoded").POST(apiUrl, this.Parameters);

            // parse the response
            if (!bool.TryParse(this.Response, out result))
            {
                this.ErrorStatus = this.GetSubscriptionStatus(this.Response);
            }

            return result;

        }

        /// <summary>
        /// Calls Sendy API to unscubscribe user
        /// API POST URL: http://your_sendy_installation/unsubscribe
        /// </summary>
        /// <param name="listId"></param>
        /// <param name="email"></param>
        /// <param name="plainTextResponse"></param>
        public bool Ubsubscribe(string listId, string email, bool plainTextResponse)
        {

            // api url
            var result = false;
            var apiUrl = Config.AppConfig.getConfig().Sendy.InstallationUrl + "/unsubscribe";

            // set the parameters to post
            this.Parameters = string.Format("list={0}&email={1}&boolean={2}", 
                listId, 
                email, 
                plainTextResponse ? "true" : "false");
           
            // post to sendy api
            this.Response = new HttpPost("application/x-www-form-urlencoded").POST(apiUrl, this.Parameters);

            // parse the response
            if (!bool.TryParse(this.Response, out result))
            {
                this.ErrorStatus = this.GetSubscriptionStatus(this.Response);
            }

            return result;

        }

        /// <summary>
        /// Calls Sendy API to get the current status of a subscriber
        /// API POST URL: http://your_sendy_installation/api/subscribers/subscription-status.php
        /// </summary>
        /// <param name="listId"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public StatusCode Status(string listId, string email)
        {

            // api url
            var apiUrl = Config.AppConfig.getConfig().Sendy.InstallationUrl + "/api/subscribers/subscription-status.php";
            // api key
            var apiKey = Config.AppConfig.getConfig().Sendy.ApiKey;

            // set the parameters to post
            this.Parameters = string.Format("list_id={0}&email={1}&api_key={2}", 
                listId, 
                email, 
                apiKey);
            
            // post to sendy api
            this.Response = new HttpPost("application/x-www-form-urlencoded").POST(apiUrl, this.Parameters);

            return GetSubscriptionStatus(this.Response);

        }

        /// <summary>
        /// Calls Sendy API to get the total active subscriber count.
        /// API POST URL: http://your_sendy_installation/api/subscribers/active-subscriber-count.php
        /// </summary>
        /// <param name="listId"></param>
        /// <returns></returns>
        public int ActiveSubscriberCount(string listId)
        {

            // api url
            var apiUrl = Config.AppConfig.getConfig().Sendy.InstallationUrl + "/api/subscribers/active-subscriber-count.php";
            // api key
            var apiKey = Config.AppConfig.getConfig().Sendy.ApiKey;

            int result = 0;   
         
            // set the parameters to post
            this.Parameters = string.Format("list_id={0}", listId);

            // post to sendy api
            this.Response = new HttpPost("application/x-www-form-urlencoded").POST(apiUrl, this.Parameters);

            // attempt to parse result to integer
            if (!int.TryParse(this.Response, out result))
            {
                this.ErrorStatus = GetSubscriptionStatus(this.Response);
            }

            return result;
            
        }
       
    }

}
