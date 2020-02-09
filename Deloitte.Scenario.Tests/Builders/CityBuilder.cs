using Deloitte.Scenario.Models.CityModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deloitte.Scenario.Tests.Builders
{
    public class CityBuilder
    {
        private readonly City _model = new City();

        public City Build()
        {
            return _model;
        }

        public CityBuilder WithId(int id)
        {
            _model.Id = id;
            return this;
        }

        public CityBuilder WithName(string name)
        {
            _model.Name = name;
            return this;
        }

        public CityBuilder WithCountry(string country)
        {
            _model.Name = country;
            return this;
        }

        public CityBuilder WithState(string state)
        {
            _model.State = state;
            return this;
        }

        public CityBuilder WithTouristRating(int rating)
        {
            _model.TouristRating = rating;
            return this;
        }

        public CityBuilder WithDateEstablished(DateTime dateEstablished)
        {
            _model.DateEstablished = dateEstablished;
            return this;
        }

        public CityBuilder WithEstimatedPopulation(long estimatedPopulation)
        {
            _model.EstimatedPopulation = estimatedPopulation;
            return this;
        }
    }
}
