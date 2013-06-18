using Hackathon.Sherlock.Alchemy;
using Hackathon.Sherlock.IronIO;
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
                    //System.Console.WriteLine("!!payload=" + payload);
                    payloadInstance = JsonConvert.DeserializeObject<AlchemyPaylaod>(payload);
                }



                //int indtest = Array.IndexOf(args, "-d");
                //if (indtest >= 0 && (indtest + 1) < args.Length)
                //{
                //    string pathTest = args[indtest + 1];
                //    System.Console.WriteLine("show files -" + File.ReadAllText(pathTest));
                //}


                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0}?apikey={1}&outputMode=json&url={2}", URLGetRankedNamedEntities, APIKey, payloadInstance.Url);
                //System.Console.WriteLine("!!call to Alchemi=" + sb.ToString());

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
                //add to Q
                //IronMQHelper iron = new IronMQHelper();

                //IronMQHelper.AddMessageReqPayload payloadReq = new IronMQHelper.AddMessageReqPayload();
                //IronMQHelper.Message ironMsg = new IronMQHelper.Message();

                ////for the body
                //ironMsg.body = JsonConvert.SerializeObject(sherlockRankList);
                //var msgList = new List<IronMQHelper.Message>();
                //msgList.Add(ironMsg);
                //payloadReq.messages = msgList;

                //System.Console.WriteLine("Hopefor the best " + JsonConvert.SerializeObject(payloadReq));

                //var msgInsertedToQStatus = iron.AddMessagesToQueue("SherlockMQProd", "dP79mahQ6lic5qetpQ3OmrohfNE", "51bbe549ed3d7679f5000282", payloadReq);
                //System.Console.WriteLine("After msg inserted to Q - " + msgInsertedToQStatus);

            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }
    }
}

