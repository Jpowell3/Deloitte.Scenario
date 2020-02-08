using System;
using System.Collections.Generic;
using System.Text;

namespace Deloitte.Scenario.TransferModels
{
    public class CityWeatherTransferModel
    {
        public string Description { get; set; }
        public decimal Temperature { get; set; }
        public decimal FeelsLike { get; set; }
        public decimal TemperatureMinimum { get; set; }
        public decimal TemperatureMaximum { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
    }
}
