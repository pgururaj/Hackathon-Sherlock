using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Hackathon.Sherlock.Alchemy;
using Hackathon.Sherlock.IronIO;

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
            var response1 = alchemy.GetResponse("The \"Hand of God\" goal happened in a soccer world cup in this city", "City");

            alchemy.CallGetRankedKeywordAPI("http://www.kcfountains.com/");
            var response = alchemy.CallGetRankedNamedEntities("http://www.kcfountains.com/", "City");
            return View();
        }

        public ActionResult IronIO()
        {
            //Iron IO Test Calls
            string output = QueueTask.queue_task("51bbe549ed3d7679f5000282", "BXxvffaWJeFwM4WTo52mt1x9OXY", "smart");

            //extract the TaskID from Output.
            
            return View();
        }

    }
}
