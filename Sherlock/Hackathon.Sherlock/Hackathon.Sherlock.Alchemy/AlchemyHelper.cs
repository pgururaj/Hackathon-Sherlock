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
        private string URLGetRankedNamedEntities = "http://access.alchemyapi.com/calls/url/URLGetRankedNamedEntities";


        private string URLGetConstraintQuery = "http://access.alchemyapi.com/calls/url/URLGetConstraintQuery";
        private string URLGetText = "http://access.alchemyapi.com/calls/url/URLGetText";

        public Dictionary<string, AlchemyWeightedData> GetResponse(string SearchParam, string Category)
        {
            var sherlockRankList = new List<AlchemyWeightedData>();

            //call google to get the search results - URL
            GoogleHelper gHelp = new GoogleHelper();
            IList<GoogleSearchResult> googleresultList = gHelp.GetSearchResults(SearchParam);

            IList<AlchemyWeightedData> aggrgAlchemyWeight = new List<AlchemyWeightedData>();

            var aggDict = new Dictionary<string, AlchemyWeightedData>();

            //iterate through the GoogleSearchResult and pass each URL to Alchemy to get a weihted score
            foreach (var googleResult in googleresultList)
            {
                var alchWtData = CallGetRankedNamedEntities(googleResult.GSearchResultURL, Category);
                foreach (var alchResponse in alchWtData)
                {
                    //if (null != aggDict[alchResponse.TextResponse])
                    if (aggDict.ContainsKey(alchResponse.TextResponse))
                    {
                        //key already exists - modify
                        var toModifyAlchemyWtData = aggDict[alchResponse.TextResponse];
                        toModifyAlchemyWtData.RelevanceScore = (toModifyAlchemyWtData.RelevanceScore + alchResponse.RelevanceScore) / 2;
                        aggDict[alchResponse.TextResponse] = toModifyAlchemyWtData;
                    }
                    else
                    {
                        //new key, just add
                        aggDict.Add(alchResponse.TextResponse, alchResponse);
                    }
                }

            }


            return aggDict;
        }

        public void CallGetRankedKeywordAPI(string ClientURL)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}?apikey={1}&outputMode=json&url={2}", URLGetRankedKeyword, APIKey, ClientURL);
            
            var request = WebHelper.GetWebRequest(sb.ToString(), null);
            var result = WebHelper.GetObjectResponse<AlchemyResponse>(request);
        }

        public IList<AlchemyWeightedData> CallGetRankedNamedEntities(string ClientURL, string Category)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}?apikey={1}&outputMode=json&url={2}", URLGetRankedNamedEntities, APIKey, ClientURL);

            var request = WebHelper.GetWebRequest(sb.ToString(), null);
            var result = WebHelper.GetObjectResponse<dynamic>(request);

            var sherlockRankList = new List<AlchemyWeightedData>();
            int counter = 0;
            
            foreach (var item in result.entities)
            {

                if (item.type == Category)
                {
                    var alchWtData = new AlchemyWeightedData { TextResponse = item.text, RelevanceScore = double.Parse(item.relevance.ToString()), Order = counter };
                    sherlockRankList.Add(alchWtData);
                    counter++;
                }
                
            }
            return sherlockRankList;
        }
    }
}
