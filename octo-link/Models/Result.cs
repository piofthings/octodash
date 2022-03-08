
namespace OctoDash.OctoLink.Models
{
    using Newtonsoft.Json;
    using System;

    public class Result
    {
        [JsonProperty("consumption")]
        public decimal Consumption { get; set; }
        [JsonProperty("intervalStart")]
        public DateTime IntervalStart { get; set; }
        [JsonProperty("intervalEnd")]
        public DateTime IntervalEnd { get; set; }

    }
}
