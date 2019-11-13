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

        public List<Klant> GetFilteredKlanten(string FilterCustomer)
        {
            using (FestiSpecEntities context = new FestiSpecEntities())
            {
                return context.Klant.Where(e => e.Naam.Contains(FilterCustomer)).ToList();
            }
        }

        public List<Klant> GetKlantenASC(string FilterCustomer)
        {
            using(FestiSpecEntities context = new FestiSpecEntities())  
            {
                return context.Klant.OrderBy(e => e.Naam).ToList();
            }
        }

        public List<Klant> GetKlantenDESC(string FilterCustomer)
        {
            using (FestiSpecEntities context = new FestiSpecEntities())
            {
                return context.Klant.OrderByDescending(e => e.Naam).ToList();
            }
        }

        public List<Klant> GetFilteredKlantenASC(string FilterCustomer)
        {
            using (FestiSpecEntities context = new FestiSpecEntities())
            {
                return context.Klant.Where(e => e.Naam.Contains(FilterCustomer)).OrderBy(e => e.Naam).ToList();
            }
        }

        public List<Klant> GetFilteredKlantenDESC(string FilterCustomer)
        {
            using (FestiSpecEntities context = new FestiSpecEntities())
            {
                return context.Klant.Where(e => e.Naam.Contains(FilterCustomer)).OrderByDescending(e => e.Naam).ToList();
            }
        }

    }
}
