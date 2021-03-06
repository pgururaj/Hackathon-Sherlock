﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hackathon.Sherlock.Web.Models
{
    public class GameRound
    {
        public Category Category{get;set;}
        public string Challenge { get; set; }
        public string CorrectResponse { get; set; }
        public int Reward { get; set; }
        public bool Used { get; set; }
    }

    public enum Category
    {
        Person,
        City
    }
}