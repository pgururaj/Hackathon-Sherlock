using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon.Sherlock.Alchemy
{
    public class GoogleSearchResults
    {
        public string GSearchResultURL { get; set; }
        public int Order { get; set; }
    }

    public class GoogleHelper
    {
        public string GetSearchResults(string searchTerm)
        {
            //var searchTerm = "ABCD";
            using (var web = new WebClient())
            {
                web.Headers.Add("Referrer", "http://your-website-here/");
                var result = web.DownloadString(String.Format(
                       "http://ajax.googleapis.com/ajax/services/search/web?v=1.0&q={0}",
                       searchTerm));


                var searchResults = JsonConvert.DeserializeObject<dynamic>(result);
                return "";
            }
        }
    }
}
