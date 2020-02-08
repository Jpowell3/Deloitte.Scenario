using Deloitte.Scenario.Models.CityModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Deloitte.Scenario.Data
{
    public interface ICityRepository
    {
        public Task<IEnumerable<City>> GetCityByNameAsync(string name);

        public Task<int> AddCityAsync(City city);

        public Task<bool> UpdateCityAsync(int Id, City city);

        public Task<bool> DeleteCityAsync(int id);

        public Task<City> GetCityByIdAsync(int id);
    }
}
