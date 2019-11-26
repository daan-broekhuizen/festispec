using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Festispec.Utility.Builders
{
    public class SettingsBuilder
    {
        public AppSettings Build()
        {
            string settingsJson = "";

            if (File.Exists("settings.local.json"))
                settingsJson = Regex.Replace(File.ReadAllText("settings.local.json"), @"(\/\*)(.{1,})(\*\/)", "");
            else if (File.Exists("settings.json"))
                settingsJson = Regex.Replace(File.ReadAllText("settings.json"), @"(\/\*)(.{1,})(\*\/)", "");
            else
                return new AppSettings() { DebugMode = false };

            return JsonConvert.DeserializeObject<AppSettings>(settingsJson);
        }
    }
}
