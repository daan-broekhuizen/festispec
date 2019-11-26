using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.API.ImageShack
{
    [JsonObject]
    public class ImageModel
    {
        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("server")]
        public int Server { get; set; }

        [JsonProperty("bucket")]
        public int Bucket { get; set; }

        [JsonProperty("filename")]
        public string FileName { get; set; }

        [JsonProperty("original_filename")]
        public string OriginalFileName { get; set; }

        [JsonProperty("direct_link")]
        public string DirectLink { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("filesize")]
        public int FileSize { get; set; }

        [JsonProperty("creation_date")]
        public int CreationDate { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }
    }
}
