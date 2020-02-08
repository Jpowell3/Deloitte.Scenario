using Deloitte.Scenario.Models.WeatherModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Deloitte.Scenario.BusinesLogic.Servcies.WeatherService
{
    public interface IWeatherService
    {
        public Task<WeatherModel> GetWeatherAsync(string cityName);
    }
}
