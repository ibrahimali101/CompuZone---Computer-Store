using Microsoft.Extensions.Configuration;
using CompuZone.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuZone.Application.Localization
{
    public class LocalizationService
    {
        private readonly IConfiguration _configuration;
        public LocalizationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private Dictionary<string, string> Application_Arabic;
        private Dictionary<string, string> Application_English;
        protected void Configure()
        {
            Application_Arabic = LoadJsonFileWithName("Arabic").GetAwaiter().GetResult();
            Application_English = LoadJsonFileWithName("English").GetAwaiter().GetResult();
        }
        public string GetString(string? key, LanguageRequest? language = LanguageRequest.English)
        {
            return language switch
            {
                (LanguageRequest.Arabic) => Application_Arabic.GetValueOrDefault(key),
                (LanguageRequest.English) => Application_English.GetValueOrDefault(key),
                (_) => string.Empty
            };
        }
        private async Task<Dictionary<string, string>> LoadJsonFileWithName(string language)
        {
            using var file = File.OpenText($"Localizations/{language}.json");
            var fileText = await file.ReadToEndAsync();
            var dict = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(fileText);
            return dict ?? new Dictionary<string, string>();
        }
    }
}
