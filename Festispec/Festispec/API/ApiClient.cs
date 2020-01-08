using Festispec.API.Uploading;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.API
{
    public class ApiClient : IClient
    {
        protected RestClient Client { get; set; }

        public ApiClient(string baseUrl)
        {
            Client = new RestClient(baseUrl);
        }

        public string Request(string endpoint, Method method, Dictionary<string, string> urlSegments = null, Dictionary<string, string> parameters = null, Dictionary<string, string> headers = null, FileData[] files = null)
        {
            RestRequest request = new RestRequest(endpoint);

            if (parameters != null)
            {
                foreach (KeyValuePair<string, string> parameter in parameters)
                    request.AddParameter(parameter.Key, parameter.Value);
            }

            if (urlSegments != null)
            {
                foreach (KeyValuePair<string, string> urlSegment in urlSegments)
                    request.AddUrlSegment(urlSegment.Key, urlSegment.Value);
            }

            if (headers != null)
            {
                foreach (KeyValuePair<string, string> header in headers)
                    request.AddHeader(header.Key, header.Value);
            }

            if (files != null)
            {
                foreach (FileData fileData in files)
                    request.AddFileBytes(fileData.Name, fileData.Data, fileData.FileName, fileData.ContentType);
            }

            return Client.Execute(request, method).Content;
        }

        public T RequestAsJsonObject<T>(string endpoint, Method method, Dictionary<string, string> urlSegments = null, Dictionary<string, string> parameters = null, Dictionary<string, string> headers = null, FileData[] files = null)
        {
            return JsonConvert.DeserializeObject<T>(Request(endpoint, method, urlSegments, parameters, headers, files));
        }

        public T RequestAsJsonObjectFromValue<T>(string endpoint, Method method, string key, Dictionary<string, string> urlSegments = null, Dictionary<string, string> parameters = null, Dictionary<string, string> headers = null, FileData[] files = null)
        {
            return JsonConvert.DeserializeObject<T>(JObject.Parse(Request(endpoint, method, urlSegments, parameters, headers, files))[key].ToString());
        }
    }
}
