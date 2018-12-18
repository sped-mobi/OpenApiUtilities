using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CkanApiHelper.Core
{
    public class ApiResult : IApiResult
    {
        [JsonProperty("help")]
        public string Help { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("result")]
        public object Result { get; set; }

        [JsonProperty("error")]
        public object Error { get; set; }
    }
    

}
