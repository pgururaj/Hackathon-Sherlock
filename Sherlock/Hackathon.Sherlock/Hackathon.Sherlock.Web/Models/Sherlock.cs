using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hackathon.Sherlock.Alchemy;

namespace Hackathon.Sherlock.Web.Models
{
    public class Sherlock:User
    {
        public Dictionary<string, AlchemyWeightedData> GetPossibleResponses()
        {
            return new Dictionary<string, AlchemyWeightedData>();
        }
    }
}