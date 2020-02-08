using Deloitte.Scenario.Models.WeatherModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Deloitte.Scenario.BusinesLogic.Servcies.WeatherService
{
    public class WeatherService : IWeatherService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;

        public WeatherService(IConfiguration config, IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));

            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public async Task<WeatherModel> GetWeatherAsync(string cityName)
        {

            using var client = _clientFactory.CreateClient();

            var url = string.Format(_config.GetValue<string>("WeatherServiceUrl"), cityName);

            client.BaseAddress = new Uri(url);

            var response = await client.GetStringAsync("");

            return JsonConvert.DeserializeObject<WeatherModel>(response);

        }
    }
}

