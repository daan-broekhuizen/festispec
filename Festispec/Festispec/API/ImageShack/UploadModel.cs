using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.API.ImageShack
{
    [JsonObject]
    public class UploadModel
    {
        [JsonProperty("max_filesize")]
        public int MaxFileSize { get; set; }

        [JsonProperty("space_limit")]
        public int SpaceLimit { get; set; }

        [JsonProperty("space_used")]
        public int SpaceUsed { get; set; }

        [JsonProperty("space_left")]
        public int SpaceLeft { get; set; }

        [JsonProperty("passed")]
        public int Passed { get; set; }

        [JsonProperty("failed")]
        public int Failed { get; set; }

        [JsonProperty("images")]
        public ImageModel[] Images { get; set; }
    }
}
