using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using C_18_01_Capstone.API.Contract;
using C_18_01_Capstone.Services;

namespace C_18_01_Capstone.Web.Services
{
    public interface IApiClient
    {
        Task<bool> CreateUser(
            CreateUserApiModel user);

        Task<UserModel> GetUser(string login);

        Task<IReadOnlyList<CountryApiModel>> GetCountries();
<<<<<<< HEAD

        Task<IReadOnlyList<CreateUserApiModel>> FindUser(string login);
=======
        
>>>>>>> d99fed55eeb362394f98f401000baee9260fab78
    }
}