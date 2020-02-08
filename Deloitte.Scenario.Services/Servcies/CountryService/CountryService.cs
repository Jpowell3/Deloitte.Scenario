using Deloitte.Scenario.Models.CountryModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Deloitte.Scenario.Services.Servcies.CountryService
{
    public class CountryService : ICountryService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;

        public CountryService(IConfiguration config, IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public async Task<IEnumerable<CountryModel>> GetCountryAsync(string countryName)
        {
            using var client = _clientFactory.CreateClient();

            var url = string.Format(_config.GetValue<string>("CountryServiceUrl"), countryName);

            client.BaseAddress = new Uri(url);

            var response = await client.GetStringAsync("");

            return JsonConvert.DeserializeObject<List<CountryModel>>(response);

        }
    }
}
