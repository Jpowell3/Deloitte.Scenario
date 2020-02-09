using AutoMapper;
using Deloitte.Scenario.Api.Controllers;
using Deloitte.Scenario.Api.Mapping;
using Deloitte.Scenario.BusinesLogic.Servcies.WeatherService;
using Deloitte.Scenario.BusinessLogic;
using Deloitte.Scenario.Data;
using Deloitte.Scenario.Services.Servcies.CountryService;
using Deloitte.Scenario.TransferModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deloitte.Scenario.Tests
{
    [TestClass]
    public class CityControllerTests
    {
        Mock<IServiceCore> _serviceCore;
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

            _serviceCore = new Mock<IServiceCore>();
        }

        [TestMethod]
        public async Task Given_A_City_Request_Verify_Service_Method_Are_Called_Once()
        {
            //Arrange
            var cityController = new CitiesController(_serviceCore.Object);
            _serviceCore.Setup(x => x.GetCityByNameAsync(It.IsAny<string>()));
            _serviceCore.Setup(x => x.AddCityAsync(It.IsAny<CityAddTransferModel>()));
            _serviceCore.Setup(x => x.UpdateCityAsync(It.IsAny<int>(), It.IsAny<CityUpdateTransferModel>()));
            _serviceCore.Setup(x => x.DeleteCityAsync(It.IsAny<int>()));
            _serviceCore.Setup(x => x.CityExistsAsync(It.IsAny<int>())).ReturnsAsync(true);

            //Act
            await cityController.GetCity("test");
            await cityController.CreateCity(new CityAddTransferModel());
            await cityController.UpdateCity(1, new CityUpdateTransferModel());
            await cityController.DeleteCity(1);

            //Assert
            _serviceCore.Verify(x => x.GetCityByNameAsync(It.IsAny<string>()), Times.Once);
            _serviceCore.Verify(x => x.GetCityByNameAsync(It.IsAny<string>()), Times.Once);
            _serviceCore.Verify(x => x.AddCityAsync(It.IsAny<CityAddTransferModel>()), Times.Once);
            _serviceCore.Verify(x => x.UpdateCityAsync(It.IsAny<int>(), It.IsAny<CityUpdateTransferModel>()), Times.Once);
            _serviceCore.Verify(x => x.DeleteCityAsync(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public async Task Given_A_Successfull_Delete_Request_ReturnsOk()
        {
            // Arrange
            _serviceCore.Setup(x => x.CityExistsAsync(It.IsAny<int>())).ReturnsAsync(true);
            _serviceCore.Setup(x => x.DeleteCityAsync(It.IsAny<int>())).ReturnsAsync(true);

            var controller = new CitiesController(_serviceCore.Object);

            // Act
            ActionResult actionResult = await controller.DeleteCity(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkResult));
        }

        [TestMethod]
        public async Task Given_A_Unsuccessfull_Delete_Request_ReturnsNotFound()
        {
            // Arrange
            _serviceCore.Setup(x => x.CityExistsAsync(It.IsAny<int>())).ReturnsAsync(false);
            _serviceCore.Setup(x => x.DeleteCityAsync(It.IsAny<int>())).ReturnsAsync(false);

            var controller = new CitiesController(_serviceCore.Object);

            // Act
            ActionResult actionResult = await controller.DeleteCity(1);
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public async Task Given_A_Valid_City_Request_ReturnsOK()
        {
            // Arrange
            _serviceCore.Setup(x => x.GetCityByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(new List<CityTransferModel>() { new CityTransferModel() { Name = "CityName" } });

            var controller = new CitiesController(_serviceCore.Object);

            // Act
            var response = await controller.GetCity("CityName");
            // Assert
            Assert.IsInstanceOfType(response, typeof(OkObjectResult));
        }
    }
}
