namespace OctoLinkTests
{
    using Moq;
    using FluentAssertions;
    using Moq.Contrib.HttpClient;
    using Newtonsoft.Json;
    using OctoLink.Services;
    using System.Net.Http;
    using Xunit;
    using System.Threading.Tasks;
    using OctoDash.OctoLink.Models;
    using OctoDash.OctoLink.Config;
    using System.Collections.Generic;

    public class OctoGasServiceTests
    {
        [Fact]
        public async Task ApiKeyIsRequiredField()
        {
            var handler = new Mock<HttpMessageHandler>();
            var client = handler.CreateClient();

            var connectorConfig = new ConnectorConfig{
                BaseUrl = "https://api.octopus.energy",
                GasMeterUrl =  "/v1/gas-meter-points/yourmeter/meters/something/consumption/",
                ElectricityMeterUrl = "/v1/electricity-meter-points/yourmeter/meters/something/consumption/",
                ApiKey = ""
            };

            handler.SetupRequest(HttpMethod.Get, $"{connectorConfig.BaseUrl}{connectorConfig.GasMeterUrl}")
                .ReturnsResponse(JsonConvert.SerializeObject(new Reading {  Results = new List<Result>(), Next = "page=2"}), "application/json");

            var octoGasService = new OctoGasService(client, connectorConfig);
            var readings = await octoGasService.GetReadings<Reading>();
            readings.Should().Be(default(Result));
        }

        [Fact]
        public async Task ApiReturnsCorrectType()
        {
            var handler = new Mock<HttpMessageHandler>();
            var client = handler.CreateClient();

            var connectorConfig = new ConnectorConfig{
                BaseUrl = "https://api.octopus.energy",
                GasMeterUrl =  "/v1/gas-meter-points/yourmeter/meters/something/consumption/",
                ElectricityMeterUrl = "/v1/electricity-meter-points/yourmeter/meters/something/consumption/",
                ApiKey = "123"
            };

            handler.SetupRequest(HttpMethod.Get, $"{connectorConfig.BaseUrl}{connectorConfig.GasMeterUrl}")
                .ReturnsResponse(JsonConvert.SerializeObject(new Reading {  Results = new List<Result>(), Next = "page=2"}), "application/json");

            var octoGasService = new OctoGasService(client, connectorConfig);
            var reading = await octoGasService.GetReadings<Reading>();
            reading.Should().NotBe(null);
            if(reading != null){
                reading.Next.Should().Be("page=2");
            }
        }
    }
}