using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using C_18_01_Capstone.API.Contract;
using C_18_01_Capstone.Services;
using C_18_01_Capstone.Web.Infrastructure;

namespace C_18_01_Capstone.Web.Services
{
    public interface IApiClient
    {
        Task<bool> CreateUser(
            CreateUserApiModel user);

        Task<UserModel> GetUser(string login);

        Task<IReadOnlyList<CountryApiModel>> GetCountries();

        Task GetAndStoreToken(string login, string hashedPassword);

        bool IsAuthenticated { get; }

        Task<string> CheckAuthorizedRoute();
    }
}