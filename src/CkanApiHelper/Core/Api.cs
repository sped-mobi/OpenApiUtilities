using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CkanApiHelper.Core
{
    internal static class Api
    {
        public const string ApiKey = "2c2e5e7f-8982-45a7-bf05-03618bed4f23";


        private static HttpWebRequest CreateRequest(string uri)
        {
            HttpWebRequest request = WebRequest.CreateHttp(uri);
            request.Headers.Add(HttpRequestHeader.Authorization, ApiKey);
            return request;
        }

        private static HttpWebResponse GetResponse(string uri)
        {
            HttpWebRequest request = CreateRequest(uri);
            return (HttpWebResponse) request.GetResponse();
        }


        public static IApiResult OrganizationList()
        {
            return ReadResponse(GetResponse(ApiPaths.OrganizationList));
        }

        private static IApiResult ReadResponse(HttpWebResponse response)
        {
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return JsonConvert.DeserializeObject<ApiResult>(reader.ReadToEnd());
            }
        }
    }
}
