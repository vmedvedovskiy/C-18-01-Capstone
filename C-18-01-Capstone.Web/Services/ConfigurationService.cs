using System;
using System.Configuration;

namespace C_18_01_Capstone.Web.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private const string ApiBasePathSettingKey = "ApiBasePath";

        public Uri ApiBasePath 
            => new Uri(ConfigurationManager.AppSettings[ApiBasePathSettingKey]);
    }
}