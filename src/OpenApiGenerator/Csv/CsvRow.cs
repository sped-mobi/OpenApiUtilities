using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace OpenApiGenerator.Csv
{
    public class CsvRow
    {
        public CsvRow()
        {
            Cells = new List<CsvCell>();
            Index = 0;
        }   

        [JsonProperty("index")]
        public int Index { get; set; }

        [JsonProperty("rowNumber")]
        public int RowNumber { get; set; }

        [JsonProperty("cells")]
        public List<CsvCell> Cells { get; set; }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("[ ");
            for (int i = 0; i < Cells.Count; i++)
            {
                var cell = Cells[i];

                sb.Append("'");
                sb.Append(cell.Value);
                sb.Append("'");


                if (i != Cells.Count - 1)
                {
                    sb.Append(", ");
                }
            }

            sb.Append(" ]");

            return sb.ToString();
        }
    }
}