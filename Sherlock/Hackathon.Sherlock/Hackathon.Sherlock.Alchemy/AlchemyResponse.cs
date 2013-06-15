using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackathon.Sherlock.Alchemy
{
    public class AlchemyResponse
    {
        public string Status { get; set; }
        public string Usage { get; set; }
        public string Url { get; set; }
        public string Language { get; set; }
        public IList<Keywords> Keywords { get; set; }
    }

    public class Keywords
    {
        public string Text { get; set; }
        public string Relevance { get; set; }
    }
}
