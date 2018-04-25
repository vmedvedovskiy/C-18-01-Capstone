using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using C_18_01_Capstone.API.Contract;
using Newtonsoft.Json;

namespace C_18_01_Capstone.Web.Services
{
    public class ApiClient : IApiClient
    {
        private const string JsonContentType = "application/json";

        private readonly IConfigurationService configuration;

        public ApiClient(IConfigurationService configuration)
        {
            this.configuration = configuration;
        }

        public async Task<bool> CreateUser(
            CreateUserApiModel user)
        {
            using (var httpClient = new HttpClient())
            {
                return await httpClient
                    .PostAsync(
                        CreateResourceUri("users"),
                        CreateApiRequest(user))
                    .ContinueWith(task => task.Result.IsSuccessStatusCode);
            }
        }

        public async Task<IReadOnlyList<CountryApiModel>> GetCountries()
        {
            using (var httpClient = new HttpClient())
            {
                var result = await httpClient
                    .GetAsync(CreateResourceUri("countries"));

                var content = await result
                    .Content.ReadAsStringAsync();

                return JsonConvert
                    .DeserializeObject<List<CountryApiModel>>(content);
            }
        }

        public async Task<IReadOnlyList<CreateUserApiModel>> FindUser(string login)
        {
            using (var httpClient = new HttpClient())
            {
                var result = await httpClient
                    .GetAsync(CreateResourceUri("users"));

                var content = await result
                    .Content.ReadAsStringAsync();

                return JsonConvert
                    .DeserializeObject<IReadOnlyList<CreateUserApiModel>>(content);
            }
        }
        
        private Uri CreateResourceUri(string resource) 
            => new Uri(this.configuration.ApiBasePath, resource);

        private HttpContent CreateApiRequest(CreateUserApiModel user)
        {
            return new StringContent(
                JsonConvert.SerializeObject(user),
                Encoding.UTF8,
                JsonContentType);
        }
    }
}