using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using C_18_01_Capstone.API.Contract;
using C_18_01_Capstone.Services;
using C_18_01_Capstone.Web.Infrastructure;
using Newtonsoft.Json;

namespace C_18_01_Capstone.Web.Services
{
    public class ApiClient : IApiClient
    {
        private const string JsonContentType = "application/json";
        private const string UrlEncodedContentType = "application/x-www-form-urlencoded";

        private readonly IConfigurationService configuration;
        private static Token token;

        public bool IsAuthenticated
        {
            get
            {
                return token != null;
            }
        }

        public ApiClient(IConfigurationService configuration)
        {
            this.configuration = configuration;
        }

        public async Task<bool> CreateUser(CreateUserApiModel user)
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

        public async Task<UserModel> GetUser(string login)
        {
            return await GetResourceAsync<UserModel>("users/" + login);
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

        private async Task<TResult> GetResourceAsync<TResult>(string resource)
        {
            using (var httpClient = new HttpClient())
            {
                if (token != null)
                {
                    httpClient.DefaultRequestHeaders.Authorization =
                        new AuthenticationHeaderValue(token.token_type, 
                                                      token.access_token);

                }

                var result = await httpClient
                        .GetAsync(CreateResourceUri(resource));

                var content = await result
                    .Content.ReadAsStringAsync();

                return JsonConvert
                    .DeserializeObject<TResult>(content);
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

        public async Task GetAndStoreToken(string login, string hashedPassword)
        {
            using (var httpClient = new HttpClient())
            {
                var content = (StringContent)CreateUrlEncodedApiRequest(
                    $"grant_type=password&login={login}&hashedPassword={hashedPassword}");

                HttpResponseMessage response = await httpClient.PostAsync
                    (CreateResourceUri("token"), content);

                string result = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    token = JsonConvert.DeserializeObject<Token>(result);
                }
                else
                {
                    throw new ApplicationException();
                }                                 
            }
        }

        private HttpContent CreateUrlEncodedApiRequest(string parameters)
        {
            return new StringContent(
                parameters,
                Encoding.UTF8,
                UrlEncodedContentType);
        }

        public async Task<string> CheckAuthorizedRoute()
        {
            return await GetResourceAsync<string>("users/authorizedRoute");
        }
    }
}