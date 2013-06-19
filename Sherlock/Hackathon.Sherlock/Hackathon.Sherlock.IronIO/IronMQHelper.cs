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

        public class MessageCreateSuccess
        {
            public string id { get; set; }
            public string name { get; set; }
            public int size {get;set;}
            public string project_id { get; set; }
            public int retries { get; set; }
            public string push_type { get; set; }
            public int retries_delay { get; set; }
        }

        public class Subscriber
        {
            public string url { get; set; }
        }

        public class MsgQRequestBody
        {
            public string push_type { get; set; }
            public IList<Subscriber> subscribers { get; set; }
        }

        public class Message
        {
            public string body { get; set; }
        }

        public class AddMessageReqPayload
        {
            public IList<Message> messages { get; set; }
        }

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
            
            using (var web = new WebClient())
            {
                web.Headers.Add("Referrer", "http://your-website-here/");
            //https://worker-aws-us-east-1.iron.io/2/projects/51bbe549ed3d7679f5000282/tasks/51bdbc35d54db3322a7c7d6f/log?oauth=dP79mahQ6lic5qetpQ3OmrohfNE
                var result = web.DownloadString(String.Format("https://worker-aws-us-east-1.iron.io/2/projects/{0}/tasks/{1}/log?oauth={2}", ProjectId, TaskId, oauthToken));

                return result;
            }
            
        }

        public MessageCreateSuccess CreateIronMQ(string qName, string oauthToken, string projectId)
        {
                                       
            string uri = string.Format("https://mq-aws-us-east-1.iron.io/1/projects/{0}/queues/{1}", projectId, qName);
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
            request.ContentType = "application/json";
            request.Headers.Add("Authorization", "OAuth " + oauthToken);
            request.UserAgent = "IronMQ .Net Client";
            request.Method = "POST";

            //string body = "{\"push_type\": \"multicast\",\"subscribers\": null}";
            MsgQRequestBody body = new MsgQRequestBody();
            body.push_type = "multicast";
            body.subscribers = null;

            var bodyStr = JsonConvert.SerializeObject(body);

            if (bodyStr != null)
            {
                using (System.IO.StreamWriter write = new System.IO.StreamWriter(request.GetRequestStream()))
                {
                    write.Write(bodyStr);
                    write.Flush();
                }
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            using (System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream()))
            {
                //return reader.ReadToEnd();

                return JsonConvert.DeserializeObject<MessageCreateSuccess>(reader.ReadToEnd());
            }
        }

        public string  AddMessagesToQueue(string qName, string oauthToken, string projectId, AddMessageReqPayload addMessageReqPayload)
        {
            string uri = string.Format("https://mq-aws-us-east-1.iron.io/1/projects/{0}/queues/{1}", projectId, qName);
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(uri);
            request.ContentType = "application/json";
            request.Headers.Add("Authorization", "OAuth " + oauthToken);
            request.UserAgent = "IronMQ .Net Client";
            request.Method = "POST";

            var body = JsonConvert.SerializeObject(addMessageReqPayload);

            if (body != null)
            {
                using (System.IO.StreamWriter write = new System.IO.StreamWriter(request.GetRequestStream()))
                {
                    write.Write(body);
                    write.Flush();
                }
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            string resultVal = "error";
            using (System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream()))
            {
                resultVal = reader.ReadToEnd();
            }
            return resultVal;
        }
    }
}

