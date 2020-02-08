using System;
using System.Collections.Generic;
using System.Text;

namespace Deloitte.Scenario.TransferModels
{
    public class CountryTransferModel
    {
        public string Alpha2Code { get; set; }
        public string Alpha3Code { get; set; }
        public IEnumerable<CountryCurrencyTransferModel> Currencies { get; set; }
    }
}
