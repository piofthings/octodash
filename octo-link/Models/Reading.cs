namespace OctoDash.OctoLink.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class Reading
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("next")]
        public string Next { get; set; } = "";

        [JsonProperty("previous")]
        public string Previous { get; set; } = "";
        
        [JsonProperty("results")]
        public IEnumerable<Result> Results { get; set; } = new List<Result>();
    }
}
