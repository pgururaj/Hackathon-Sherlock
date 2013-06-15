using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hackathon.Sherlock.Web.Models;
using Microsoft.AspNet.SignalR;

namespace Hackathon.Sherlock.Web
{
    public class GameHub : Hub
    {
        public void SendChallenge()
        {
            var gameRound = Game.GetNextRound();
            Clients.All.newChallenge(new Random().Next(100000));
        }

        public void SendUserResponse(string sessionId, string response)
        {
            Clients.All.handleResponse(sessionId, response);
        }


        public void IsGameFull()
        {
            Clients.All.isGameFull(Game.Users.Count > 3);
        }
    }
}   