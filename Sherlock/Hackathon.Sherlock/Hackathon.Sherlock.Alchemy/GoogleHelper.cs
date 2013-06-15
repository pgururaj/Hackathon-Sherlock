using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon.Sherlock.Alchemy
{
    public class GoogleSearchResult
    {
        public string GSearchResultURL { get; set; }
        public string Title { get; set; }
        public int Order { get; set; }
    }

    public class GoogleHelper
    {
        public IList<GoogleSearchResult> GetSearchResults(string searchTerm)
        {
            var resultList = new List<GoogleSearchResult>();
            //var searchTerm = "ABCD";
            using (var web = new WebClient())
            {
                web.Headers.Add("Referrer", "http://your-website-here/");
                var result = web.DownloadString(String.Format(
                       "http://ajax.googleapis.com/ajax/services/search/web?v=1.0&q={0}",
                       searchTerm));


                var searchResults = JsonConvert.DeserializeObject<dynamic>(result);

                if (searchResults.responseData != null && searchResults.responseData.results != null)
                {
                    int count = 0;
                    foreach (var item in searchResults.responseData.results)
                    {
                        resultList.Add(new GoogleSearchResult { 
                                GSearchResultURL = item.url, 
                                Title = item.titleNoFormatting, 
                                Order = count 
                        });
                        count++;
                    }
                }
                
            }

            return resultList;
        }
    }
}
