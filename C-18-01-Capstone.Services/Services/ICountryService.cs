using System.Collections.Generic;

namespace C_18_01_Capstone.Services.Services
{
    public interface ICountryService
    {
        IReadOnlyList<CountryModel> GetAllCountries();
    }
}
