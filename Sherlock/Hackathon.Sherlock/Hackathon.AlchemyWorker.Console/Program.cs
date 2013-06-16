using Hackathon.Sherlock.Alchemy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon.AlchemyWorker.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //AlchemyHelper alchemy = new AlchemyHelper();
                string APIKey = "8da86f0a977a22e600739f6f693b39fddefbd503";

                string URLGetRankedNamedEntities = "http://access.alchemyapi.com/calls/url/URLGetRankedNamedEntities";



                //extract the payload

                int ind = Array.IndexOf(args, "-payload");
                var payloadInstance = new AlchemyPaylaod();
                if (ind >= 0 && (ind + 1) < args.Length)
                {
                    string path = args[ind + 1];
                    string payload = File.ReadAllText(path);
                    System.Console.WriteLine("!!payload=" + payload);
                    payloadInstance = JsonConvert.DeserializeObject<AlchemyPaylaod>(payload);
                }


                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0}?apikey={1}&outputMode=json&url={2}", URLGetRankedNamedEntities, APIKey, payloadInstance.Url);
                System.Console.WriteLine("!!call to Alchemi=" + sb.ToString());

                var request = WebHelper.GetWebRequest(sb.ToString(), null);
                var result = WebHelper.GetObjectResponse<dynamic>(request);

                var sherlockRankList = new List<AlchemyWeightedData>();
                int counter = 0;

                foreach (var item in result.entities)
                {

                    if (item.type == payloadInstance.Category)
                    {
                        var alchWtData = new AlchemyWeightedData { TextResponse = item.text, RelevanceScore = double.Parse(item.relevance.ToString()), Order = counter };
                        sherlockRankList.Add(alchWtData);
                        counter++;
                    }

                }
                System.Console.WriteLine(JsonConvert.SerializeObject(sherlockRankList));
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }
    }
}




//int ind = Array.IndexOf(args, "-payload");
//        if( ind >= 0 && (ind+1) < args.Length ){
//            string path = args[ind+1];
//            string payload = File.ReadAllText(path);
//            JavaScriptSerializer serializer = new JavaScriptSerializer();
//            IDictionary<string,string> json = serializer.Deserialize <Dictionary<string, string>>(payload);
//            foreach (string key in json.Keys)
//            {
//                Console.WriteLine( key + " = " + json[key] );
//            }
//        }

