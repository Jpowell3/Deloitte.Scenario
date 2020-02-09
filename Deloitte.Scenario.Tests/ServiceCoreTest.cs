using AutoMapper;
using Deloitte.Scenario.Api.Mapping;
using Deloitte.Scenario.BusinesLogic.Servcies.WeatherService;
using Deloitte.Scenario.BusinessLogic;
using Deloitte.Scenario.Data;
using Deloitte.Scenario.Models.CityModels;
using Deloitte.Scenario.Models.CountryModels;
using Deloitte.Scenario.Models.WeatherModels;
using Deloitte.Scenario.Services.Servcies.CountryService;
using Deloitte.Scenario.Tests.Builders;
using Deloitte.Scenario.TransferModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deloitte.Scenario.Tests
{
    [TestClass]
    public class ServiceCoreTest
    {
        IMapper _mapper;
        Mock<IWeatherService> _weatherService;
        Mock<ICountryService> _countryService;
        Mock<ICityRepository> _cityRepository;

        [TestInitialize]
        public void Setup()
        {
            var mapperProfle = new CityMappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(mapperProfle));
            _mapper = new Mapper(configuration);

            _weatherService = new Mock<IWeatherService>();
            _countryService = new Mock<ICountryService>();
            _cityRepository = new Mock<ICityRepository>();

        }


        [TestMethod]
        public async Task Given_A_City_Request_Validate_City_Mapped()
        {
            //Arrange
            var expectedCities = new List<City>  {
                            new CityBuilder().WithId(1)
                                             .WithName("CityA")
                                             .WithCountry("CounterA")
                                             .WithTouristRating(1)
                                             .WithEstimatedPopulation(100)
                                             .WithDateEstablished(new DateTime(1800,1,1))
                                             .Build()};

            var service = new ServiceCore(_cityRepository.Object, _weatherService.Object, _countryService.Object, _mapper);
            _cityRepository.Setup(x => x.GetCityByNameAsync(It.IsAny<string>())).ReturnsAsync(expectedCities);

            //Act
            var cities = (List<CityTransferModel>)await service.GetCityByNameAsync("CityName");

            //Assert
            Assert.IsInstanceOfType(cities[0], typeof(CityTransferModel));

            Assert.AreEqual(expectedCities[0].Id, cities[0].Id);
            Assert.AreEqual(expectedCities[0].Name, cities[0].Name);
            Assert.AreEqual(expectedCities[0].TouristRating, cities[0].TouristRating);
            Assert.AreEqual(expectedCities[0].EstimatedPopulation, cities[0].EstimatedPopulation);
            Assert.AreEqual(expectedCities[0].DateEstablished, cities[0].DateEstablished);
        }

        [TestMethod]
        public async Task Given_A_City_Request_Validate_CountryInformation_Returned()
        {
            var mockCities = new List<City>  {
                                        new CityBuilder().WithId(1)
                                                         .WithName("CityA")
                                                         .WithCountry("CounterA")
                                                         .WithTouristRating(1)
                                                         .WithEstimatedPopulation(100)
                                                         .WithDateEstablished(new DateTime(1800,1,1))
                                                         .Build()};

            var expectedCountryInfo = new List<CountryModel>  {
                                    new CountryBuilder()
                                        .WithAlpha2Code("AA")
                                        .WithAlpha3Code("AAA")
                                        .WithCurrency("GBP", "British Pound", "£")
                                        .Build()
                                    };

            _countryService.Setup(x => x.GetCountryAsync(It.IsAny<string>())).ReturnsAsync(expectedCountryInfo);

            var service = new ServiceCore(_cityRepository.Object, _weatherService.Object, _countryService.Object, _mapper);

            _cityRepository.Setup(x => x.GetCityByNameAsync(It.IsAny<string>())).ReturnsAsync(mockCities);

            //Act
            var cities = await service.GetCityByNameAsync("CityName");
            var actualCountryInfo = cities.FirstOrDefault().CountryInformation.FirstOrDefault();

            //Assert
            Assert.AreEqual(actualCountryInfo.Alpha2Code, "AA");
            Assert.AreEqual(actualCountryInfo.Alpha3Code, "AAA");
            Assert.AreEqual(actualCountryInfo.Currencies.FirstOrDefault().Code, "GBP");
            Assert.AreEqual(actualCountryInfo.Currencies.FirstOrDefault().Name, "British Pound");
            Assert.AreEqual(actualCountryInfo.Currencies.FirstOrDefault().Symbol, "£");
        }

        [TestMethod]
        public async Task Given_A_City_Update_Request_Validate_City_Mapped()
        {
            //Arrange
            var cityUpdate = new CityUpdateTransferModel()
            {
                TouristRating = 1,
                DateEstablished = new DateTime(2000, 1, 1),
                EstimatedPopulation = 500
            };

            var mappedCity = new City();
            var service = new ServiceCore(_cityRepository.Object, _weatherService.Object, _countryService.Object, _mapper);

            _cityRepository.Setup(x => x.UpdateCityAsync(It.IsAny<int>(), It.IsAny<City>()))
                           .Callback<int, City>((id, city) =>
                           {
                               mappedCity = city;
                           }).ReturnsAsync(true);

            //Act
            var cities = await service.UpdateCityAsync(1, cityUpdate);

            //Assert
            Assert.AreEqual(cityUpdate.TouristRating, mappedCity.TouristRating);
            Assert.AreEqual(cityUpdate.EstimatedPopulation, mappedCity.EstimatedPopulation);
            Assert.AreEqual(cityUpdate.DateEstablished, mappedCity.DateEstablished);
        }

        [TestMethod]
        public async Task Given_A_City_Request_Validate_WeatherInformation_Returned()
        {
            var mockCities = new List<City>  {
                                        new CityBuilder().WithId(1)
                                                         .WithName("CityA")
                                                         .WithCountry("CounterA")
                                                         .WithTouristRating(1)
                                                         .WithEstimatedPopulation(100)
                                                         .WithDateEstablished(new DateTime(1800,1,1))
                                                         .Build()};

            var expectedWeatherInfo = new WeatherBuilder()
                                                .WithDescription("Cloudy")
                                                .WithTemperature(15.6F)
                                                .WithMaximumTemperature(16)
                                                .WithMinimumTemperature(15)
                                                .WithPressure(1012)
                                                .WithHimidity(75)
                                                .Build();

            _weatherService.Setup(x => x.GetWeatherAsync(It.IsAny<string>())).ReturnsAsync(expectedWeatherInfo);

            var service = new ServiceCore(_cityRepository.Object, _weatherService.Object, _countryService.Object, _mapper);

            _cityRepository.Setup(x => x.GetCityByNameAsync(It.IsAny<string>())).ReturnsAsync(mockCities);

            //Act
            var cities = await service.GetCityByNameAsync("CityName");
            var actualWeatherInfo = cities.FirstOrDefault().Weather;

            //Assert
            Assert.AreEqual(actualWeatherInfo.Description, "Cloudy");
            Assert.AreEqual(actualWeatherInfo.Temperature, 15.6M);
            Assert.AreEqual(actualWeatherInfo.TemperatureMaximum, 16);
            Assert.AreEqual(actualWeatherInfo.TemperatureMinimum, 15);
            Assert.AreEqual(actualWeatherInfo.Humidity, 75);
            Assert.AreEqual(actualWeatherInfo.Pressure, 1012);
        }

    }


}

