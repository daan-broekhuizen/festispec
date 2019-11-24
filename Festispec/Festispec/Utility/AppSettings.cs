using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Utility
{
    [JsonObject]
    public class AppSettings
    {
        [JsonProperty("debugMode")]
        public bool DebugMode { get; set; }

        [JsonProperty("account")]
        public User Account { get; set; }

        [JsonObject]
        public struct User
        {
            [JsonProperty("username")]
            public string Username { get; set; }

            [JsonProperty("password")]
            public string Password { get; set; }
        }

    }
}
