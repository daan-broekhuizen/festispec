using Festispec.Model.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Model.Repositories
{
    public class OfflineJobRepository
    {
        public List<JsonJob> GetOfflineOpdrachten(string path)
        {
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<List<JsonJob>>(json);
        }
    }
}
