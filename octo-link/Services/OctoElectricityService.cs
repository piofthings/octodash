namespace OctoLink.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
    using OctoDash.OctoLink.Config;

    public class OctoElectricityService : IOctoElectricityService
    {
        private readonly HttpClient _httpClient;
        private readonly ConnectorConfig _connectorConfig;
        public static string BaseUrl = "";
        public static string ApiUrl = "";

        public OctoElectricityService(HttpClient httpClient, ConnectorConfig connectorConfig)
        {
            this._httpClient = httpClient;
            this._connectorConfig = connectorConfig;
        }

        public async Task<T?> GetReadings<T>()
        {
            this._httpClient.BaseAddress = new Uri(this._connectorConfig.BaseUrl);
            this._httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", this._connectorConfig.ApiKey);
            var message = await this._httpClient.GetAsync(ApiUrl);
            T? returnValue = await JsonSerializer.DeserializeAsync<T?>(message.Content.ReadAsStream()); 
            return returnValue;
        }
    }
}
