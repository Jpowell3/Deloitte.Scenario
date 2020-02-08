using Deloitte.Scenario.TransferModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Deloitte.Scenario.BusinessLogic
{
    public interface IServiceCore
    {
        public Task<IEnumerable<CityTransferModel>> GetCityByNameAsync(string name);

        public Task<int> AddCityAsync(CityAddTransferModel city);

        public Task<bool> UpdateCityAsync(int id, CityUpdateTransferModel city);

        public Task<bool> CityExistsAsync(int id);

        public Task<bool> DeleteCityAsync(int id);
    }
}
