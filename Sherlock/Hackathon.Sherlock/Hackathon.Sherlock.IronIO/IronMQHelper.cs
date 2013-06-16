using Hackathon.Sherlock.IronIO.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon.Sherlock.IronIO
{
    public class IronMQHelper
    {
        public QueueTaskResponse queue_tasks(string projectId, string token, QueueTaskRequest tasks)
        {
            string uri = "https://worker-aws-us-east-1.iron.io:443/2/projects/" + projectId + "/tasks";
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
            request.ContentType = "application/json";
            request.Headers.Add("Authorization", "OAuth " + token);
            request.UserAgent = "IronMQ .Net Client";
            request.Method = "POST";
            // We hand code the JSON payload here. You can automatically convert it, if you prefer
            //string body = "{\"tasks\": [ { \"code_name\": \"" + worker + "\", \"payload\": \"{\\\"key\\\": \\\"value\\\", \\\"fruits\\\": [\\\"apples\\\", \\\"oranges\\\"]}\"} ] }";

            string body = JsonConvert.SerializeObject(tasks);
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
                //return reader.ReadToEnd();

                return JsonConvert.DeserializeObject<QueueTaskResponse>(reader.ReadToEnd());
            }
        }

        public string GetTaskResponse(string ProjectId, string TaskId, string oauthToken)
        {
            //https://worker-aws-us-east-1.iron.io/2/projects/51bbe549ed3d7679f5000282/tasks/51bd71110865524a8c7c5cca?oauth=BXxvffaWJeFwM4WTo52mt1x9OXY
            using (var web = new WebClient())
            {
                web.Headers.Add("Referrer", "http://your-website-here/");
                var result = web.DownloadString(String.Format("https://worker-aws-us-east-1.iron.io/2/projects/{0}/tasks/{1}?oauth={2}", ProjectId, TaskId, oauthToken));

                return result;
            }
            
        }
    }
}
