﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Erp.Models
{
    public class EventViewModel
    {
        public Int64 id { get; set; }

        public string title { get; set; }

        public string start { get; set; }

        public string end { get; set; }

        public bool allDay { get; set; }
    }
}
