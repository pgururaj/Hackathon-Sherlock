using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hackathon.Sherlock.Web.Models;

namespace Hackathon.Sherlock.Web
{
    public static class AllGameChallenges
    {

        public static readonly IList<GameRound> AllGameRounds = new List<GameRound>
        { 
         new GameRound { Category = Category.People, Challenge="This person from Kansas City created Ethereal (now WireShark)", CorrectResponse="Gerard Combs",Reward=600 } ,
         new GameRound { Category = Category.People, Challenge="This person from Kansas City created Hallmark", CorrectResponse="Joyce Hall", Reward=600} ,
         new GameRound { Category = Category.People, Challenge="This person co-founded Microsoft with Bill Gates", CorrectResponse="Paul Allen", Reward=200 } ,
         new GameRound { Category = Category.Places, Challenge="This place known as the city of fountains", CorrectResponse="Kansas City", Reward=200 } ,
         new GameRound { Category = Category.Places, Challenge="This city has the tallest building in the world", CorrectResponse="Dubai", Reward=400 } ,
         new GameRound { Category = Category.Places, Challenge="This river is the longest in the world", CorrectResponse="Nile" , Reward=200} ,
         new GameRound { Category = Category.Places, Challenge="This place is the capital of New Zealand", CorrectResponse="Wellington", Reward=800 },
         new GameRound { Category = Category.Places, Challenge="The \"Hand of God\" goal happened in a soccer world cup in this city", CorrectResponse="Mexico City", Reward=800 }
        };
    }
}