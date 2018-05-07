using System.Collections.Generic;
using System.Threading.Tasks;
using C_18_01_Capstone.API.Contract;
using C_18_01_Capstone.API.Infrastructure;
using C_18_01_Capstone.Services;

namespace C_18_01_Capstone.Web.Services
{
    public interface IApiClient
    {
        Task<bool> CreateUser(
            CreateUserApiModel user);

        Task<UserModel> GetUser(string login);

        Task<IReadOnlyList<CountryApiModel>> GetCountries();

        Task<Token> GetToken(string login, string hashedPassword);
    }
}