using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Hackathon.Sherlock.Alchemy;
using Hackathon.Sherlock.IronIO;
using Hackathon.Sherlock.IronIO.Models;
using Newtonsoft.Json;

namespace Hackathon.Sherlock.Web.Controllers
{
    public class SandboxController : Controller
    {
        //
        // GET: /Sandbox/

        public ActionResult Index()
        {
            //Alchemy Test Calls
            AlchemyHelper alchemy = new AlchemyHelper();
            //var response1 = alchemy.GetResponse("The \"Hand of God\" goal happened in a soccer world cup in this city", "City");

            var response2 = alchemy.GetResponse("City of Fountains", "City");

            //alchemy.CallGetRankedKeywordAPI("http://www.kcfountains.com/");
            //var response = alchemy.CallGetRankedNamedEntities("http://www.kcfountains.com/", "City");
            return View();
        }

        public class SamplePaylaod
        {
            public string Url { get; set; }
            public string Category { get; set; }
        }

        public ActionResult IronIO()
        {
            //Iron IO Test Calls
            //string output = QueueTask.queue_task("51bbe549ed3d7679f5000282", "BXxvffaWJeFwM4WTo52mt1x9OXY", "smart");


            //string outputAlch = QueueTask.queue_task("51bbe549ed3d7679f5000282", "BXxvffaWJeFwM4WTo52mt1x9OXY", "alchemy");

            
            IronMQHelper iron = new IronMQHelper();

            var payload = new SamplePaylaod { Url = @"http://www.kcfountains.com/", Category = "City" };

            var task1 = new QueueTaskRequest.Task() {
                code_name = "alchemy", payload = JsonConvert.SerializeObject(payload)
            };

            var tasksToBeQueued = new QueueTaskRequest();
            var listOfTasks = new List<QueueTaskRequest.Task>();
            listOfTasks.Add(task1);
            tasksToBeQueued.tasks = listOfTasks;

            var response = iron.queue_tasks("51bbe549ed3d7679f5000282", "BXxvffaWJeFwM4WTo52mt1x9OXY", tasksToBeQueued);

            //extract the TaskID from Output.
            
            return View();
        }

    }
}
