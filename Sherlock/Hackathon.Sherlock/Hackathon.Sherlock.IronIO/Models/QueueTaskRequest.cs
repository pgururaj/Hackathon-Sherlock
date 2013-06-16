using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon.Sherlock.IronIO.Models
{
    public class QueueTaskRequest
    {
        public class Task
        {
            public string code_name { get; set; }
            public string payload { get; set; }
        }

        public IList<Task> tasks { get; set; }
    }

    public class QueueTaskResponse
    {
        public class Task
        {
            public string id { get; set; }
        }
        public IList<Task> tasks { get; set; }
    }

    public class TaskInfo
    {
        public string id { get; set; }
        public string project_id { get; set; }
        public string code_id { get; set; }
        public string code_history_id { get; set; }
        public string status { get; set; }
        public string code_name { get; set; }
        public string code_rev { get; set; }
        public string start_time { get; set; }
        public string end_time { get; set; }
        public string duration { get; set; }
        public string timeout { get; set; }
        public string payload { get; set; }
        public string updated_at { get; set; }
        public string created_at { get; set; }
    }


    
}
