using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Model.Repositories
{
    public class OfflineInspectionsRepository
    {
        public List<Klant> GetOfflineCustomers()
        {
            using(IDbConnection cnn = new SQLiteConnection())
            {

            }
        }
    }

    private static string LoadConnectionString(string id = "Default")
    {
        return ConfigurationManager.ConnectionStrings[].ConnectionString
    }
}
