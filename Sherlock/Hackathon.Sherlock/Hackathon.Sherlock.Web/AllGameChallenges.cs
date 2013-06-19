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
         new GameRound { Category = Category.Person, Challenge="This person from Kansas City created Ethereal (now WireShark)", CorrectResponse="Gerard Combs",Reward=600, Used=false } ,
         new GameRound { Category = Category.Person, Challenge="This Russian authored \"War and Peace\"", CorrectResponse="Leo Tolstoy", Reward=600, Used=false} ,
         new GameRound { Category = Category.Person, Challenge="This person co-founded Microsoft with Bill Gates", CorrectResponse="Paul Allen", Reward=200, Used=false } ,
         new GameRound { Category = Category.Person, Challenge="This person is known as the fastest man on earth", CorrectResponse="Usain Bolt", Reward=200, Used=false } ,
         //new GameRound { Category = Category.Person, Challenge="This place known as the city of fountains", CorrectResponse="Kansas City", Reward=200, Used=false } ,
         new GameRound { Category = Category.City, Challenge="This small middle eastern city has the tallest building in the world", CorrectResponse="Dubai", Reward=400, Used=false } ,
         new GameRound { Category = Category.City, Challenge="This city is named after Romulus and Remus", CorrectResponse="Rome" , Reward=200, Used=false} ,
         new GameRound { Category = Category.City, Challenge="This place is the capital of New Zealand", CorrectResponse="Wellington", Reward=800, Used=false }
         ,new GameRound { Category = Category.City, Challenge="The \"Hand of God\" goal happened in a soccer world cup held this latin American city", CorrectResponse="Mexico City", Reward=800, Used=false }
        };
    }
}