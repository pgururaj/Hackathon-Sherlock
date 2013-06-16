using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Hackathon.Sherlock.Alchemy;

namespace Hackathon.Sherlock.Web.Controllers
{
    public class SandboxController : Controller
    {
        //
        // GET: /Sandbox/

        public ActionResult Index()
        {
            AlchemyHelper alchemy = new AlchemyHelper();
            var response1 = alchemy.GetResponse("city of fountains", "City");

            alchemy.CallGetRankedKeywordAPI("http://www.kcfountains.com/");
            var response = alchemy.CallGetRankedNamedEntities("http://www.kcfountains.com/", "City");
            
            return View();
        }

    }
}
