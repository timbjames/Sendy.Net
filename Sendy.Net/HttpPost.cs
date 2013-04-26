using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Sendy.Net
{
    public class HttpPost
    {

        public string ContentType { get; set; }

        /// <summary>
        /// Set the Content Type
        /// e.g. application/x-www-form-urlencoded
        /// </summary>
        /// <param name="contentType"></param>
        public HttpPost(string contentType)
        {
            ContentType = contentType;
        }

        /// <summary>
        /// POSTs paramters to specified url
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public string POST(string url, string parameters)
        {
            string response = string.Empty;
            WebClient wc = null;

            try
            {
                using (wc = new WebClient())
                {
                    wc.Headers[HttpRequestHeader.ContentType] = ContentType;
                    response = wc.UploadString(url, parameters);
                }
            }
            catch (Exception ex)
            {
                // log
            }
            finally
            {
                if (wc != null)
                {
                    wc.Dispose();
                    wc = null;
                }
            }
            return response;

        }

    }
}
