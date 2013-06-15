using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon.Sherlock.Alchemy
{
    public class AlchemyHelper
    {
        private string APIKey = "8da86f0a977a22e600739f6f693b39fddefbd503";
        private string URLGetRankedKeyword = "http://access.alchemyapi.com/calls/url/URLGetRankedKeywords";
        private string URLGetConstraintQuery = "http://access.alchemyapi.com/calls/url/URLGetConstraintQuery";
        private string URLGetText = "http://access.alchemyapi.com/calls/url/URLGetText";

        public void CallGetRankedKeywordAPI(string ClientURL)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}?apikey={1}&outputMode=json&url={2}", URLGetRankedKeyword, APIKey, ClientURL);
            
            var request = WebHelper.GetWebRequest(sb.ToString(), null);
            var result = WebHelper.GetObjectResponse<AlchemyResponse>(request);
        }
    }
}
