using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Model.Repositories
{
    public class StatusRepository
    {
        public List<Status> GetAllStatus()
        {
            using (FestispecContext context = new FestispecContext())
            {
                return context.Status.ToList();
            }
        } 

        public string GetStatus(string Betekenis)
        {
            using (FestispecContext context = new FestispecContext())
            {
                return context.Status.Where(e => e.Betekenis == Betekenis).Select(e => e.Afkorting).FirstOrDefault();
            }
        }
    }
}
