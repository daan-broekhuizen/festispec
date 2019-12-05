using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Model.Repositories
{
    public class Temp
    {
        public void Test(string festival)
        {
            using (FestispecContext context = new FestispecContext())
            {
                var param = new SqlParameter("@Event", festival);
                Account acc = new Account();
               
                var result = context.Database.SqlQuery<Account>("GetInspectors @Event", param).ToList();

                foreach(var r in result)
                {
                    Console.WriteLine(r);
                }
            }
        }
    }
}
