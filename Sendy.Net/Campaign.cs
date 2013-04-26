using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sendy.Net
{
    
    public class Campaign : Sendy
    {
                
        public Campaign()
        {
        }

        /// <summary>
        /// Calls Sendy API to create a campaign within a brand
        /// API POST URL: http://your_sendy_installation/api/campaigns/create-campaign.php
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="subject"></param>
        /// <param name="fromName"></param>
        /// <param name="plainText"></param>
        /// <param name="htmlText"></param>
        /// <returns></returns>
        public bool Create(string brandEmailAddressLogin, string subject, string fromName, string plainText, string htmlText)
        {

            var result = false;

            // api url            
            var apiUrl = Config.AppConfig.getConfig().Sendy.InstallationUrl + "/api/campaigns/create-campaign.php";
            // api key
            var apiKey = Config.AppConfig.getConfig().Sendy.ApiKey;
            
            // set the parameters to post
            this.Parameters = string.Format("api_key={0}&user_name={1}&subject={2}&from_name={3}&plain_text={4}&html_text={5}",
                apiKey, brandEmailAddressLogin, subject, fromName, plainText, htmlText);

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
        /// Gets a list of campaigns for specific brand
        /// API POST URL: http://your_sendy_installation/api/campaigns/list-campaigns.php
        /// </summary>
        /// <param name="brandEmailAddressLogin"></param>
        /// <returns></returns>
        public string List(string brandEmailAddressLogin)
        {
            
            // api url            
            var apiUrl = Config.AppConfig.getConfig().Sendy.InstallationUrl + "/api/campaigns/list-campaigns.php";
            // api key
            var apiKey = Config.AppConfig.getConfig().Sendy.ApiKey;
            
            // set the parameters to post
            this.Parameters = string.Format("api_key={0}&user_name={1}",
                apiKey, brandEmailAddressLogin);

            // post info to sendy api
            this.Response = new HttpPost("application/x-www-form-urlencoded").POST(apiUrl, this.Parameters);

            // result should be a list of campaigns, comma seperated. check for match against ErrorStatus
            // if there is an ErrorStatus, then there was an error

            this.ErrorStatus = this.GetSubscriptionStatus(this.Response);

            return this.Response;

        }

    }

}
