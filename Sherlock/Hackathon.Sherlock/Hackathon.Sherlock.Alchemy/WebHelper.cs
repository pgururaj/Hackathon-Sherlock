using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon.Sherlock.Alchemy
{
    public static class WebHelper
    {
        private static void SetPostHeader(string content, HttpWebRequest req)
        {
            if (!String.IsNullOrEmpty(content))
            {
                byte[] buffer = Encoding.ASCII.GetBytes(content);
                req.ContentLength = buffer.Length;
                Stream postData = req.GetRequestStream();
                postData.Write(buffer, 0, buffer.Length);
                postData.Close();
            }
        }

        public static HttpWebRequest GetWebRequest(string uri, string headerObject)
        {
            HttpWebRequest req = WebRequest.Create(uri) as HttpWebRequest;
            req.KeepAlive = false;
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";

            SetPostHeader(headerObject, req);

            return req;
        }

        public static T GetObjectResponse<T>(HttpWebRequest postRequest)
        {
            WebResponse webResponse = postRequest.GetResponse();


            string json = new StreamReader(webResponse.GetResponseStream()).ReadToEnd();
            T jobStatusResponse = JsonConvert.DeserializeObject<T>(json);



            return jobStatusResponse;
        }
    }
}
