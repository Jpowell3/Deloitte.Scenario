using Deloitte.Scenario.Models.CountryModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Deloitte.Scenario.Services.Servcies.CountryService
{
    public interface ICountryService
    {
        public Task<IEnumerable<CountryModel>> GetCountryAsync(string countryName);
    }
}
