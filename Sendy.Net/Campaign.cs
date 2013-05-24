using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sendy.Net
{
    
   public class Campaign : Sendy
    {
            
        /// <summary>
        /// <para>Sendy Campaign API is a downloaded plugin from http://forum.sendy.co/discussion/768/added-some-api-functionality/p1 </para>
        /// <para>This plugin has been modified to fix bugs, and allow for campaigns to be scheduled</para>
        /// </summary>
        public Campaign()
        {
        }

        /// <summary>
        /// <para>Calls Sendy API to create a campaign within a brand</para>
        /// <para>API POST URL: http://your_sendy_installation/api/campaigns/create-campaign.php </para>
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="subject"></param>
        /// <param name="fromName"></param>
        /// <param name="plainText"></param>
        /// <param name="htmlText"></param>
        /// <returns></returns>
        public int Create(string brandEmailAddressLogin, string subject, string fromName, string plainText, string htmlText)
        {
                        
            var campaignId = 0;

            // api url            
            var apiUrl = Config.AppConfig.getConfig().Sendy.InstallationUrl + "/api/campaigns/create-campaign.php";
            
            // api key
            var apiKey = Config.AppConfig.getConfig().Sendy.ApiKey;
            
            // set the parameters to post
            this.Parameters = string.Format("api_key={0}&user_name={1}&subject={2}&from_name={3}&plain_text={4}&html_text={5}",
                apiKey, brandEmailAddressLogin, subject, fromName, plainText, htmlText);

            // post info to sendy api            
            this.Response = new HttpPost("application/x-www-form-urlencoded").POST(apiUrl, this.Parameters);
            
            // this.response should be an integer falue with the campaign id

            // parse the response
            if (!int.TryParse(this.Response, out campaignId))
            {
                this.ErrorStatus = this.GetSubscriptionStatus(this.Response);
            }
            
            return campaignId;

        }

        /// <summary>
        /// <para>Gets a list of campaigns for specific brand</para>
        /// <para>API POST URL: http://your_sendy_installation/api/campaigns/list-campaigns.php </para>
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

        /// <summary>
        /// <para>Calls Sendy API to schedule a campaign within a brand</para>
        /// <para>API POST URL: http://your_sendy_installation/api/campaigns/schedule-campaign.php </para>
        /// <para>Please note that sendy calculates it as +1 hour from what you set. So if scheduling for 8am, set time to 7am</para>
        /// </summary>
        /// <param name="branchEmailAddressLogin"></param>
        /// <param name="campaignId"></param>
        /// <param name="sendDate"></param>
        /// <param name="emailLists"></param>
        /// <param name="timezone"></param>
        /// <returns></returns>
        public bool Schedule(string brandEmailAddressLogin, string campaignId, DateTime sendDate, string emailLists, string timezone)
        {

            bool result = false;

            // api url            
            var apiUrl = Config.AppConfig.getConfig().Sendy.InstallationUrl + "/api/campaigns/schedule-campaign.php";
            
            // api key
            var apiKey = Config.AppConfig.getConfig().Sendy.ApiKey;
            
            // set the parameters to post
            this.Parameters = string.Format("api_key={0}&user_name={1}&campaign_id={2}&send_date={3}&email_lists={4}&timezone={5}",
                apiKey, brandEmailAddressLogin,campaignId, GetUnixTimeStamp(sendDate), emailLists, timezone);

            // need to convert our DateTime varialbe into  the UNIX timestamp
            // see http://stackoverflow.com/questions/1062528/php-mktime-and-microtime-equivalent-in-c-sharp for more info
            
            // post info to sendy api            
            this.Response = new HttpPost("application/x-www-form-urlencoded").POST(apiUrl, this.Parameters);

            if (!bool.TryParse(this.Response, out result)){
                this.ErrorStatus = this.GetSubscriptionStatus(this.Response);
            }

            return result;

        }

        /// <summary>
        /// Sendy works with Unix Timestamps. This method will get the format for the date
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private string GetUnixTimeStamp(DateTime dt)
        {
            DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan span = dt - UnixEpoch;
            return ((int)span.TotalSeconds).ToString();
        }

    }

}
