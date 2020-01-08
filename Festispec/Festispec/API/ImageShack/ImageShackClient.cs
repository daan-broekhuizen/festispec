using Festispec.API.Uploading;
using Festispec.Utility.Builders;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.API.ImageShack
{
    public class ImageShackClient : ApiClient, IUploadClient
    {
        private readonly string _apiKey;

        public ImageShackClient() : base("https://api.imageshack.com/v2/")
        {
            _apiKey = (new SettingsBuilder()).Build().ApiKeys.ImageShackApiKey;
        }

        public ImageModel GetImage(string id)
        {
            Dictionary<string, string> urlSegments = new Dictionary<string, string>
            {
                { "id", id }
            };

            return RequestAsJsonObjectFromValue<ImageModel>("images/{id}", Method.GET, "result", urlSegments);
        }

        public UploadModel UploadImage(FileData[] files)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                { "api_key", _apiKey }
            };

            return RequestAsJsonObjectFromValue<UploadModel>("images", Method.POST, "result", null, parameters, null, files);
        }

        public UploadModel UploadImage(ImageContainer[] fileLocations)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                { "api_key", _apiKey }
            };

            FileData[] fileData = new FileData[fileLocations.Length];

            for (int i = 0; i < fileLocations.Length; i++)
            {
                ImageContainer file = fileLocations[i];

                string filePath = file.ImageLocation.Replace("%20", " ");
                fileData[i] = new FileData(File.ReadAllBytes(filePath), file.ContentType);
            }

            return RequestAsJsonObjectFromValue<UploadModel>("images", Method.POST, "result", null, parameters, null, fileData);
        }
    }
}
