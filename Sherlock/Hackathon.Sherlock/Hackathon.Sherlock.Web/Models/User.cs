using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hackathon.Sherlock.Web.Models
{
    public class User
    {
        public string SessionId { get; set; }
        public long Money { get; set; }
        public string Name { get; set; }
    }
}