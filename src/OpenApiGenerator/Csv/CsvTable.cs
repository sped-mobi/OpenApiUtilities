using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Newtonsoft.Json;
using Formatting = System.Xml.Formatting;

namespace OpenApiGenerator.Csv
{
    public class CsvTable
    {
        public CsvTable()
        {
            Rows = new List<CsvRow>();
        }

        [JsonProperty("rows")]
        public List<CsvRow> Rows { get; set; }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"[Row Count: {Rows.Count}]";
        }

    }
}