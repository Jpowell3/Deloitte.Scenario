using AutoMapper;
using Deloitte.Scenario.BusinesLogic.Servcies.WeatherService;
using Deloitte.Scenario.Data;
using Deloitte.Scenario.Models.CityModels;
using Deloitte.Scenario.Models.CountryModels;
using Deloitte.Scenario.Models.WeatherModels;
using Deloitte.Scenario.Services.Servcies.CountryService;
using Deloitte.Scenario.TransferModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Deloitte.Scenario.BusinessLogic
{
    public class ServiceCore : IServiceCore
    {
        private readonly ICityRepository _cityRepository;
        private readonly IWeatherService _weatherService;
        private readonly ICountryService _countryService;
        private readonly IMapper _mapper;

        public ServiceCore(ICityRepository cityRepository, IWeatherService weatherService, ICountryService countryService, IMapper mapper)
        {
            _cityRepository = cityRepository ?? throw new ArgumentNullException(nameof(cityRepository));
            _weatherService = weatherService ?? throw new ArgumentNullException(nameof(weatherService));
            _countryService = countryService ?? throw new ArgumentNullException(nameof(countryService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<int> AddCityAsync(CityAddTransferModel cityTransfer)
        {
            var city = _mapper.Map<CityAddTransferModel, City>(cityTransfer);

            return await _cityRepository.AddCityAsync(city);
        }

        public async Task<bool> CityExistsAsync(int id)
        {
            var city = await _cityRepository.GetCityByIdAsync(id);

            return city != null;
        }

        public async Task<bool> DeleteCityAsync(int id)
        {
            return await _cityRepository.DeleteCityAsync(id);
        }

        public async Task<IEnumerable<CityTransferModel>> GetCityByNameAsync(string name)
        {
            var cityTransfer = _mapper.Map<IEnumerable<City>, IEnumerable<CityTransferModel>>(await _cityRepository.GetCityByNameAsync(name));

            cityTransfer.ToList().ForEach(c =>
                       {
                           var weatherInfoTask = _weatherService.GetWeatherAsync(name);
                           var countryInfoTask = _countryService.GetCountryAsync(c.Country);
                           var allTasks = new List<Task>() { weatherInfoTask, countryInfoTask };

                           Task.WaitAll(allTasks.ToArray());

                           c.Weather = _mapper.Map<WeatherModel, CityWeatherTransferModel>(weatherInfoTask.Result);
                           c.CountryInformation = _mapper.Map<IEnumerable<CountryModel>, IEnumerable<CountryTransferModel>>(countryInfoTask.Result);
                       });

            return cityTransfer;
        }

        public async Task<bool> UpdateCityAsync(int id, CityUpdateTransferModel cityUpdate)
        {
            return await _cityRepository.UpdateCityAsync(id, _mapper.Map<CityUpdateTransferModel, City>(cityUpdate));
        }

    }
}
