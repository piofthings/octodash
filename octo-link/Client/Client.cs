namespace OctoDash.OctoLink.Client;

using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

using OctoDash.OctoLink.Config;
using OctoDash.OctoLink.Models;

public class Client : IClient
{
    private readonly HttpClient _httpClient;
    private readonly IConnector _connnector;
    public Client(IConnector connector, HttpClient client)
    {
        this._connnector = connector;
        this._httpClient = client;
    }
    public async Task<IList<ElectricityReadings>> GetElectricityReadings()
    {
        var returnValue = await BasicAuthGet<IList<ElectricityReadings>>(this._connnector.Config.ElectricityMeterUrl, this._connnector.Config.ApiKey, string.Empty);
        if (returnValue == null)
        {
            return new List<ElectricityReadings>();
        }
        return returnValue;
    }
    public async Task<IList<GasReadings>> GetGasReadings()
    {
        var returnValue = await BasicAuthGet<IList<GasReadings>>(this._connnector.Config.GasMeterUrl, this._connnector.Config.ApiKey, string.Empty);
        if (returnValue == null)
        {
            return new List<GasReadings>();
        }
        return returnValue;
    }

    public async Task<T?> BasicAuthGet<T>(string destinationUrl, string userName, string password)
    {
        var streamTask = this._httpClient.GetStreamAsync(destinationUrl);
        var repositories = await JsonSerializer.DeserializeAsync<T?>(await streamTask);
        return repositories;
    }

}