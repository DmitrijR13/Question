﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionnaire.WebSite.Core.Grid
{
    public class Rule
    {
        public string field { get; set; }

        public string op { get; set; }

        public string data { get; set; }
    }
}