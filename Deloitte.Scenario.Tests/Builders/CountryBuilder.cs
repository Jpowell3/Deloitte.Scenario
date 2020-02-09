using Deloitte.Scenario.Models.CountryModels;
using Deloitte.Scenario.Models.WeatherModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deloitte.Scenario.Tests.Builders
{
    public class CountryBuilder
    {
        private readonly CountryModel _model = new CountryModel();
        private List<Currency> _currencies = new List<Currency>();

        public CountryModel Build()
        {
            _model.currencies = _currencies.ToArray();
            return _model;
        }

        public CountryBuilder WithAlpha2Code(string code)
        {
            _model.alpha2Code = code;
            return this;
        }

        public CountryBuilder WithAlpha3Code(string code)
        {
            _model.alpha3Code = code;
            return this;
        }

        public CountryBuilder WithCurrency(string code, string name, string symbol)
        {
            _currencies.Add(new Currency() { code = code, name = name, symbol = symbol });
            return this;
        }
    }
}
