using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestiSpec.Domain.Repositories
{
    public class CustomerRepository
    {
        public List<Klant> GetKlanten()
        {
            using(FestiSpecEntities context = new FestiSpecEntities())
            {
                return context.Klant.ToList();
            }
        }
    }
}
