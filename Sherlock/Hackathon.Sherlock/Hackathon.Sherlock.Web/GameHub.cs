using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hackathon.Sherlock.Web.Models;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;
using Hackathon.Sherlock.Alchemy;

namespace Hackathon.Sherlock.Web
{
    public class GameHub : Hub
    {
        public void SendChallenge()
        {
            System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            var data = js.Serialize(Game.GetChallenge());
            Clients.All.newChallenge(data);
            
        }

        public void GetSherlockResponses()
        {

            if (Game.Users.Where(a => a.SessionId == "sherlock").FirstOrDefault() == null)
                Game.AddUser(new SherlockUser { IsPlayer = true, Money = 0, Name = "Sherlock", SessionId = "sherlock" });
            SherlockUser sh = (SherlockUser)Game.Users.Where(a => a.SessionId == "sherlock").FirstOrDefault();

            var responseDicListFromAlchemy = sh.GetPossibleResponses(Game.CurrentChallenge);
            //string serializedData = JsonConvert.SerializeObject(responseDicListFromAlchemy);

            //ABid Code//var response = sh.GetPossibleResponses(Game.CurrentChallenge).FirstOrDefault().Value;


            var listOfAlchResponses = new List<AlchemyWeightedData>();

            foreach (var dictItem in responseDicListFromAlchemy)
            {
                var key = dictItem.Key;
                var value = dictItem.Value;
                listOfAlchResponses.Add(value);
            }

            var response = listOfAlchResponses.OrderByDescending(a => a.RelevanceScore);

            //System.Web.Script.Serialization.JavaScriptSerializer js = new System.Web.Script.Serialization.JavaScriptSerializer();
            //var data = js.Serialize(response);
            Clients.All.sherlockResponse(JsonConvert.SerializeObject(response));

            if (Game.HasGameStarted())
            {
                var correctReponse = Game.GetCorrectResponse();

                //if the response is current, log the current user and end the current round.
                //if (correctReponse.ToLower() == response.TextResponse.Trim().ToLower())
                if (correctReponse.ToLower() == response.FirstOrDefault().TextResponse.Trim().ToLower())
                {
                    Game.SetChallengeWinnner("sherlock");
                    Game.EndRound();
                    Clients.All.getWinner("sherlock");
                }

            }
        }

       /* public void SendUserResponse(string sessionId, string response)
        {
            Clients.All.handleResponse(sessionId, response);
        }*/


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

            Clients.All.userAdded(sessionId, name);
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