using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using C_18_01_Capstone.API.Contract;

namespace C_18_01_Capstone.Web.Services
{
    public interface IApiClient
    {
        Task<bool> CreateUser(
            CreateUserApiModel user);

        Task<IReadOnlyList<CountryApiModel>> GetCountries();

        Task<IReadOnlyList<CreateUserApiModel>> FindUser(string login);
    }
}