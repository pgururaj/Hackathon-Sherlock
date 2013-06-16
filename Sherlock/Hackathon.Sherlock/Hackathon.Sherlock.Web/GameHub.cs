﻿using System;
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
            Clients.All.newChallenge(Game.GetChallenge());
            
        }

        public void GetSherlockResonses()
        {
            SherlockUser sh = (SherlockUser)Game.Users.Where(a => a.SessionId == "sherlock").FirstOrDefault();
            var response = sh.GetPossibleResponses(Game.CurrentChallenge);
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            var data = js.Serialize(response);
            Clients.All.sherlockResponse(data);
        }

        public void SendUserResponse(string sessionId, string response)
        {
            Clients.All.handleResponse(sessionId, response);
        }


        public void IsGameFull()
        {
            Clients.All.isGameFull(Game.Users.Count > 3);
        }

        public void StartGame()
        {
            Game.StartGame();
            Clients.All.startGame();
        }

        public void GetCategories()
        {
            if (Game.HasGameStarted())
            {
                System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
                var data=js.Serialize(Enum.GetNames(typeof(Category)));
                Clients.All.loadCategories(data);
            }
        }


        public void SetCategory(string category)
        {
            if (Game.HasGameStarted())
            {
                Game.CurrentCategory = (Category)Enum.Parse(typeof(Category), category, true);
            }            
        }

        public void GetCurrentCategory()
        {
            if (Game.HasGameStarted())
            {
                Clients.All.getCurrentCategory(Game.CurrentCategory.ToString());
            }
        }

        public void SendResponse(string sessionId,string response)
        {

            if (Game.HasGameStarted())
            {
                var correctReponse = Game.GetCorrectResponse();

                //if the response is current, log the current user and end the current round.
                if (correctReponse.ToLower() == response.Trim().ToLower())
                {                    
                    Game.SetChallengeWinnner(sessionId);
                    Game.EndRound();
                    Clients.All.getWinner(sessionId);
                }

                Clients.All.getUserResponse(sessionId, response);
            }

        }

        public void AddUserToGame(string sessionId,string name)
        {
            Game.AddUser(new User{ SessionId=sessionId, Name=name, Money=0});
            if (Game.Users.Count > 3)
                StartGame();
        }

        public void GetUserStatus(string sessionId)
        {
            var cid = Context.ConnectionId;
            var userStatus = Game.GetUserStatus(sessionId);
            Clients.Client(cid).setUserStatus(userStatus);
        }

        public void GetCurrentPicker()
        {
            var currentPicker = Game.CurrentPicker;
            if(currentPicker!=null)
                Clients.All.setCurrentPicker(currentPicker.Name,currentPicker.SessionId);
        }

        public void GetGameWinner()
        {
            var winner = Game.GetGameWinner();
            if (winner != null)
                Clients.All.setWinner(winner.SessionId);
        }

        public void EndGame()
        {
            Game.EndGame();
            Clients.All.endGame();
        }
    }
}   