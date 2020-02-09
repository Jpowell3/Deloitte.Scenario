using Deloitte.Scenario.Models.WeatherModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deloitte.Scenario.Tests.Builders
{
    public class WeatherBuilder
    {
        private readonly WeatherModel _model = new WeatherModel();
        private readonly List<Weather> _weather = new List<Weather>();

        public WeatherBuilder()
        {
            _model.main = new Main();
        }

        public WeatherModel Build()
        {
            _model.weather = _weather.ToArray();
            return _model;
        }

        public WeatherBuilder WithDescription(string description)
        {
            _weather.Add(new Weather() { description = description });
            return this;
        }

        public WeatherBuilder WithTemperature(float temperature)
        {
            _model.main.temp = temperature;
            return this;
        }

        public WeatherBuilder WithPressure(int pressure)
        {
            _model.main.pressure = pressure;
            return this;
        }

        public WeatherBuilder WithHimidity (int humidity)
        {
            _model.main.humidity = humidity;
            return this;
        }

        public WeatherBuilder WithMaximumTemperature (float maxTemp)
        {
            _model.main.temp_max = maxTemp;
            return this;
        }

        public WeatherBuilder WithMinimumTemperature(float minTemp)
        {
            _model.main.temp_min = minTemp;
            return this;
        }
    }
}
