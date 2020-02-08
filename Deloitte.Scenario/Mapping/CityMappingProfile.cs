using AutoMapper;
using Deloitte.Scenario.Data.Entities;
using Deloitte.Scenario.Models.CityModels;
using Deloitte.Scenario.Models.CountryModels;
using Deloitte.Scenario.Models.WeatherModels;
using Deloitte.Scenario.TransferModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Deloitte.Scenario.Api.Mapping
{
    public class CityMappingProfile : Profile
    {
        public CityMappingProfile()
        {
            CreateMap<CityEntity, City>().ReverseMap();

            CreateMap<City, CityTransferModel>().ReverseMap();

            CreateMap<CityAddTransferModel, City>();

            CreateMap<CityUpdateTransferModel, City>();

            CreateMap<WeatherModel, CityWeatherTransferModel>()
                .ForMember(x => x.Description, opt => opt.MapFrom(src => src.weather[0].description))
                .ForMember(x => x.Temperature, opt => opt.MapFrom(src => src.main.temp))
                .ForMember(x => x.FeelsLike, opt => opt.MapFrom(src => src.main.feel_like))
                .ForMember(x => x.TemperatureMaximum, opt => opt.MapFrom(src => src.main.temp_max))
                .ForMember(x => x.TemperatureMinimum, opt => opt.MapFrom(src => src.main.temp_min))
                .ForMember(x => x.Humidity, opt => opt.MapFrom(src => src.main.humidity))
                .ForMember(x => x.Pressure, opt => opt.MapFrom(src => src.main.pressure));

            CreateMap<Currency, CountryCurrencyTransferModel>()
                .ForMember(x => x.Code, opt => opt.MapFrom(src => src.code))
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.name))
                .ForMember(x => x.Symbol, opt => opt.MapFrom(src => src.symbol));

            CreateMap<CountryModel, CountryTransferModel>();

            CreateMap<List<CountryModel>, List<CountryTransferModel>>();
        }

    }
}

