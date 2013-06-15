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


        public static void InitializeGame()
        {
           // LoadGameRound();

            //AllGameChallenges.AllGameRounds;
        }

        public void AddUser(User user)
        {
            if (Users.Count < 4)
            {
                Users.Add(user);
            }
        }

        public Category CurrentCategory{get;set;}
    }
}