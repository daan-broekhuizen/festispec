using Festispec.API.ImageShack;
using Festispec.API.Uploading;
using Festispec.Utility.Builders;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.API.ImageBB
{
    public class ImageBBClient : ApiClient, IUploadClient
    {
        private readonly string _apiKey;

        public ImageBBClient() : base("https://api.imgbb.com/1/")
        {
            _apiKey = (new SettingsBuilder()).Build().ApiKeys.ImageBBApiKey;
        }

        public ImageModel GetImage(string id)
        {
            return new ImageModel();
        }

        public UploadModel UploadImage(FileData[] files)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                { "key", _apiKey }
            };

            for (int i = 0; i < files.Length; i++)
                parameters.Add("image", Convert.ToBase64String(files[i].Data));

            JObject imageObject = RequestAsJsonObjectFromValue<JObject>("upload", Method.POST, "data", null, parameters);
            return new UploadModel()
            {
                Images = new ImageModel[]
                {
                    new ImageModel() { DirectLink = imageObject["url"].ToString().Replace("https://", "") }
                }
            };
        }

        public UploadModel UploadImage(ImageContainer[] fileLocations)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                { "key", _apiKey }
            };

            for (int i = 0; i < fileLocations.Length; i++)
                parameters.Add("image", Convert.ToBase64String(File.ReadAllBytes(fileLocations[i].ImageLocation.Replace("%20", " "))));

            JObject imageObject = RequestAsJsonObjectFromValue<JObject>("upload", Method.POST, "data", null, parameters);

            return new UploadModel()
            {
                Images = new ImageModel[]
                {
                    new ImageModel() { DirectLink = imageObject["url"].ToString().Replace("https://", "") }
                }
            };
        }
    }
}
