﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Deloitte.Scenario.Models.WeatherModels
{
    public class Sys
    {
        public int type { get; set; }
        public int id { get; set; }
        public float message { get; set; }
        public string country { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
    }
}
