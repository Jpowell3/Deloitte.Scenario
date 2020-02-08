using System;
using System.Collections.Generic;
using System.Text;

namespace Deloitte.Scenario.Models.CityModels
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public int TouristRating { get; set; }
        public DateTime DateEstablished { get; set; }
        public long EstimatedPopulation { get; set; }
    }
}
