using System;
using System.Collections.Generic;
using System.Linq;
using C_18_01_Capstone.Main.DataAccessLayer;
using C_18_01_Capstone.Main.DataContext;
using C_18_01_Capstone.Services.Services;

namespace C_18_01_Capstone.Services.Implementation.Services
{
    public class CountryService : ICountryService
    {
        IDataAccess<Country> dataAccess;

        public CountryService(
            IDataAccess<Country> dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        public IReadOnlyList<CountryModel> GetAllCountries()
        {
            return this.dataAccess.GetEntities()
                .Select(_ => new CountryModel
                {
                    CountryIsoCode3 = _.CountryIsoCode3,
                    Name = _.Name
                })
                .ToList()
                .AsReadOnly();
        }
    }
}
