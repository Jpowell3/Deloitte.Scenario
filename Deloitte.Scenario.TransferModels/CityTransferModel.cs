using System;
using System.Collections.Generic;

namespace Deloitte.Scenario.TransferModels
{
    public class CityTransferModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public int TouristRating { get; set; }
        public DateTime DateEstablished { get; set; }
        public long EstimatedPopulation { get; set; }
        public CityWeatherTransferModel Weather { get; set; } = new CityWeatherTransferModel();
        public IEnumerable<CountryTransferModel> CountryInformation { get; set; } = new List<CountryTransferModel>();
    }
}
