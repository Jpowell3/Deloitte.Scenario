using System;
using System.Collections.Generic;
using System.Text;

namespace Deloitte.Scenario.Models.CountryModels
{
    public class CountryModel
    {
        public Currency[] currencies { get; set; }
        public string alpha2Code { get; set; }
        public string alpha3Code { get; set; }
    }
}
