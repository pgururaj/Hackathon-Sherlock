using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hackathon.Sherlock.Alchemy;

namespace Hackathon.Sherlock.Web.Models
{
    public class SherlockUser:User
    {
        public Dictionary<string, AlchemyWeightedData> GetPossibleResponses(GameRound gr)
        {
            AlchemyHelper ah = new AlchemyHelper();
            return ah.GetResponse(gr.Challenge, gr.Category.ToString());
        }

        
    }
}