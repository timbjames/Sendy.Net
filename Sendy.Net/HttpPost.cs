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
        /// <para>Set the Content Type</para>
        /// <para>e.g. application/x-www-form-urlencoded</para>
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
                //Log.ErrorLog.Error(ex);
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

        //public string POST(string url, NameValueCollection parameters, string filePath)
        //{

        //    using (var stream = File.Open(filePath, FileMode.Open))
        //    {
                
        //        var files = new[]{
        //            new UploadFile{
        //                Name = "file",
        //                Filename = Path.GetFileName(filePath),
        //                ContentType = "text/plain",
        //                Stream = stream
        //            }
        //        };
                
        //        var values = parameters;

        //        byte[] result = UploadFile.UploadFiles(url, files, values);

        //    }

        //    return "test";

        //}

    }

    //public class UploadFile
    //{

    //    public UploadFile()
    //    {
    //        ContentType = "application/octet-stream";
    //    }
    //    public string Name { get; set; }
    //    public string Filename { get; set; }
    //    public string ContentType { get; set; }
    //    public Stream Stream { get; set; }

    //    public static byte[] UploadFiles(string address, IEnumerable<UploadFile> files, NameValueCollection values)
    //    {
            
    //        var request = WebRequest.Create(address);
    //        request.Method = "POST";
    //        var boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x", NumberFormatInfo.InvariantInfo);
    //        request.ContentType = "multipart/form-data; boundary=" + boundary;
    //        boundary = "--" + boundary;

    //        using (var requestStream = request.GetRequestStream())
    //        {
                
    //            // Write the values
    //            foreach (string name in values.Keys)
    //            {
    //                var buffer = Encoding.ASCII.GetBytes(boundary + Environment.NewLine);
    //                requestStream.Write(buffer, 0, buffer.Length);
    //                buffer = Encoding.ASCII.GetBytes(string.Format("Content-Disposition: form-data; name=\"{0}\"{1}{1}", name, Environment.NewLine));
    //                requestStream.Write(buffer, 0, buffer.Length);
    //                buffer = Encoding.UTF8.GetBytes(values[name] + Environment.NewLine);
    //                requestStream.Write(buffer, 0, buffer.Length);
    //            }

    //            // Write the files
    //            foreach (var file in files)
    //            {
    //                var buffer = Encoding.ASCII.GetBytes(boundary + Environment.NewLine);
    //                requestStream.Write(buffer, 0, buffer.Length);
    //                buffer = Encoding.UTF8.GetBytes(string.Format("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"{2}", file.Name, file.Filename, Environment.NewLine));
    //                requestStream.Write(buffer, 0, buffer.Length);
    //                buffer = Encoding.ASCII.GetBytes(string.Format("Content-Type: {0}{1}{1}", file.ContentType, Environment.NewLine));
    //                requestStream.Write(buffer, 0, buffer.Length);
    //                file.Stream.CopyTo(requestStream);
    //                buffer = Encoding.ASCII.GetBytes(Environment.NewLine);
    //                requestStream.Write(buffer, 0, buffer.Length);
    //            }

    //            var boundaryBuffer = Encoding.ASCII.GetBytes(boundary + "--");
    //            requestStream.Write(boundaryBuffer, 0, boundaryBuffer.Length);

    //        }

    //        using (var response = request.GetResponse())
            
    //        using (var responseStream = response.GetResponseStream())
            
    //        using (var stream = new MemoryStream())
    //        {
    //            responseStream.CopyTo(stream);
    //            return stream.ToArray();
    //        }

    //    }

    //}
}
