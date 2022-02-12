namespace OctoDash.OctoLink.Client;

using OctoDash.OctoLink.Models;
public interface IClient
{
    Task<IList<ElectricityReadings>> GetElectricityReadings();
    Task<IList<GasReadings>> GetGasReadings();
    Task<T?> BasicAuthGet<T>(string destinationUrl, string userName, string password);
} 