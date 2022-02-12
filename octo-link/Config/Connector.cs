namespace OctoDash.OctoLink.Config;
using System.Net.Http;
using System.Net.Http.Headers;

public class Connector : IConnector
{
    public ConnectorConfig Config { get; set; } = new ConnectorConfig();

}
