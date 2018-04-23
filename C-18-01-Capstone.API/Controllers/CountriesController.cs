using System.Linq;
using System.Net;
using System.Web.Http;
using C_18_01_Capstone.API.Contract;
using C_18_01_Capstone.Services.Services;

namespace C_18_01_Capstone.API.Controllers
{
    [RoutePrefix("api/v1/countries")]
    public class CountriesController : ApiController
    {
        private readonly ICountryService countryService;

        public CountriesController(
            ICountryService countryService)
        {
            this.countryService = countryService;
        }

        [HttpGet]
        [Route]
        public IHttpActionResult GetCountries()
        {
            var countries = this.countryService
                .GetAllCountries()
                .Select(_ => new CountryApiModel
                {
                    CountryId = _.CountryIsoCode3,
                    Name = _.Name
                })
                .ToList();

            return this.Content(
                HttpStatusCode.OK,
                countries);
        }
    }
}