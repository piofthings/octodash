namespace OctoLink.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using OctoDash.OctoLink.Config;

    public class OctoGasService : IOctoGasService
    {
        public static string BaseUrl = "https://api.octopus.energy";
        public static string ApiUrl = "";
        private readonly HttpClient _httpClient;
        private readonly ConnectorConfig _connectorConfig;

        public OctoGasService(HttpClient httpClient, ConnectorConfig connectorConfig)
        {
            _httpClient = httpClient;
            _connectorConfig = connectorConfig;
        }
        public async Task<T?> GetReadings<T>()
        {
            if (string.IsNullOrEmpty(this._connectorConfig.ApiKey))
            {
                return default(T);
            }
            this._httpClient.BaseAddress = new Uri(this._connectorConfig.BaseUrl);
            this._httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", this._connectorConfig.ApiKey);
            var message = await this._httpClient.GetAsync(this._connectorConfig.GasMeterUrl);
            T? returnValue = JsonConvert.DeserializeObject<T>(await message.Content.ReadAsStringAsync());
            return returnValue;
        }
    }
}
