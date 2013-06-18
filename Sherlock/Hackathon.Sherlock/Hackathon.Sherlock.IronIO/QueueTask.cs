using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon.Sherlock.IronIO
{
    public static class QueueTask
    {
        public static string queue_task(string projectId, string token, string worker)
        {
            string uri = "https://worker-aws-us-east-1.iron.io:443/2/projects/" + projectId + "/tasks";
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
            request.ContentType = "application/json";
            request.Headers.Add("Authorization", "OAuth " + token);
            request.UserAgent = "IronMQ .Net Client";
            request.Method = "POST";
            // We hand code the JSON payload here. You can automatically convert it, if you prefer
            string body = "{\"tasks\": [ { \"code_name\": \"" + worker + "\", \"payload\": \"{\\\"key\\\": \\\"value\\\", \\\"fruits\\\": [\\\"apples\\\", \\\"oranges\\\"]}\"} ] }";
            if (body != null)
            {
                using (System.IO.StreamWriter write = new System.IO.StreamWriter(request.GetRequestStream()))
                {
                    write.Write(body);
                    write.Flush();
                }
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            using (System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream()))
            {
                return reader.ReadToEnd();
            }
        }

        static public void Main(string[] args)
        {
            //Console.WriteLine(queue_task("51bbe549ed3d7679f5000282", "BXxvffaWJeFwM4WTo52mt1x9OXY", "smart.worker"));
        }
    }
}
