using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Hackathon.Sherlock.Web
{
    public class JeopardyHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
    }
}