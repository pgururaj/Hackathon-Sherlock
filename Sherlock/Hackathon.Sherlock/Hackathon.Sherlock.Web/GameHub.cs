using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace Hackathon.Sherlock.Web
{
    public class GameHub : Hub
    {
        public void SendChallenge()
        {
            Clients.All.newChallenge(new Random().Next(100000));
        }

        public void SendUserResponse(string sessionId, string response)
        {
            Clients.All.handleResponse(sessionId, response);
        }

        public void GetSessionId()
        {
            var context = HttpContext.Current;
            var sessionId = context.Session.SessionID;
           // var client=Clients.Client(Context.ConnectionId);
        }
    }
}   