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
            alchemy.CallGetRankedKeywordAPI("http://en.wikipedia.org/wiki/Robert_Wadlow");
            
            return View();
        }

    }
}
