using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace OpenApiGenerator.Csv
{
    public class CsvCell
    {

        [JsonProperty("position")]
        public int Position { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        /// <summary>Returns the fully qualified type name of this instance.</summary>
        /// <returns>The fully qualified type name.</returns>
        public override string ToString()
        {
            return Value;
        }
    }
}