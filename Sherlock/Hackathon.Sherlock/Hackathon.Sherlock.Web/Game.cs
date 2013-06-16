using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hackathon.Sherlock.Web.Models;

namespace Hackathon.Sherlock.Web
{
    public static class Game
    {
        public static readonly int  MaxUsers=4;
        public static IList<User> Users = new List<User>();

        private static bool gameStarted=false;
        public static void StartGame()
        {
            if (!gameStarted)
                gameStarted = true;

            Random rn = new Random();
            var index=rn.Next(1,3);
            CurrentPicker = Users.Where(a => a.IsPlayer).ToList()[index-1];

            currentResponses = null;
        }

        public static bool HasGameStarted()
        {
            return gameStarted;
        }

        public static void InitializeGame()
        {
           // LoadGameRound();

            //AllGameChallenges.AllGameRounds;


        }

        public static User CurrentPicker { get; set; }

        public static void AddUser(User user)
        {

            if (Users.Where(a => a.SessionId == user.SessionId).Count() < 1)
            {
                if (Users.Count < 4)
                {
                    user.IsPlayer = true;

                }

                else
                    user.IsPlayer = false;
                Users.Add(user);
            }

        }

        public static Category CurrentCategory{get;set;}

        internal static GameRound GetNextRound()
        {
            return new GameRound();
        }



        internal static string GetChallenge()
        {
            //it can't be the first one. get the unused one in that category
            var currentChallenge = AllGameChallenges.AllGameRounds.Where(a => a.Category == Game.CurrentCategory && a.Used==false).FirstOrDefault();
            CurrentChallenge = currentChallenge;
            currentChallenge.Used = true;
            currentResponses = null;
            return currentChallenge.Challenge;
        }

        public static GameRound CurrentChallenge { get; set; }

        //dictionary of sessionIds and responses
        public static Dictionary<string, string> currentResponses = new Dictionary<string, string>();
        public static Dictionary<string, string> CurrentResponses{
            get { return currentResponses; }
            set
            {
                if (currentResponses == null)
                    currentResponses = new Dictionary<string, string>();
                currentResponses = value; 
            }
        }


        public static void ReceiveResponse(string sessionId,string response)
        {
            if (!CurrentResponses.ContainsKey(sessionId))
            {
                CurrentResponses.Add(sessionId, response);
            }
        }

        public static User LastWinner;
        public static void SetChallengeWinnner(string sessionId)
        {
            var user = Users.Where(a => a.SessionId == sessionId).FirstOrDefault();
            if (user != null)
            {
                user.Money = user.Money + CurrentChallenge.Reward;
                LastWinner = user;
                CurrentPicker = user;
            }
            
        }

        public static string GetCorrectResponse()
        {
            return CurrentChallenge.CorrectResponse;
        }


        public static User GetChallengeWinnner()
        {
            return LastWinner;
        }


        internal static void EndRound()
        {
            LastWinner = null;
            CurrentResponses = null;
            CurrentChallenge = null;
        }

        internal static bool GetUserStatus(string sessionId)
        {
            var user= Users.Where(a => a.SessionId == sessionId).FirstOrDefault();
            if (user != null)
                return user.IsPlayer;
            return false;
        }


    }
}