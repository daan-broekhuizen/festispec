using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestiSpec.Domain.Repositories
{
    public class JobRepository
    {
        public List<Opdracht> GetOpdrachten()
        {
            using (FestiSpecEntities context = new FestiSpecEntities())
            {
                return context.Opdracht.ToList();
            }
        }
    }
}
