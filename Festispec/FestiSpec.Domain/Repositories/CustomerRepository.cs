using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestiSpec.Domain.Repositories
{
    public class CustomerRepository
    {
        public List<Klant> GetCustomers()
        {
            using(FestiSpecEntities context = new FestiSpecEntities())
            {
                return context.Klant.Include("Contactpersoon").ToList();
            }
        }

        public void UpdateCustomer(Klant klant)
        {
            using(FestiSpecEntities context = new FestiSpecEntities())
            {
                context.Entry(context.Klant.Where(c => c.KvK_nummer == klant.KvK_nummer).First()).CurrentValues.SetValues(klant);
                context.SaveChanges();
            }
        }

        public void CreateCustomer(Klant klant)
        {
            using (FestiSpecEntities context = new FestiSpecEntities())
            {
                context.Klant.Add(klant);
                context.SaveChanges();
            }
        }

        public void AddContactPerson(Contactpersoon contactpersoon)
        {
            using (FestiSpecEntities context = new FestiSpecEntities())
            {
                context.Contactpersoon.Add(contactpersoon);
                context.SaveChanges();
            }
        }

        #region Filters
        public List<Klant> GetFilteredCustomers(string FilterCustomer)
        {
            using (FestiSpecEntities context = new FestiSpecEntities())
            {
                return context.Klant.Include("Contactpersoon").Where(e => e.Naam.Contains(FilterCustomer)).ToList();
            }
        }

        public List<Klant> GetKlantenASC(string FilterCustomer)
        {
            using (FestiSpecEntities context = new FestiSpecEntities())
            {
                return context.Klant.Include("Contactpersoon").OrderBy(e => e.Naam).ToList();
            }
        }

        public List<Klant> GetKlantenDESC(string FilterCustomer)
        {
            using (FestiSpecEntities context = new FestiSpecEntities())
            {
                return context.Klant.Include("Contactpersoon").OrderByDescending(e => e.Naam).ToList();
            }
        }

        public List<Klant> GetFilteredKlantenASC(string FilterCustomer)
        {
            using (FestiSpecEntities context = new FestiSpecEntities())
            {
                return context.Klant.Include("Contactpersoon").Where(e => e.Naam.Contains(FilterCustomer)).OrderBy(e => e.Naam).ToList();
            }
        }

        public List<Klant> GetFilteredKlantenDESC(string FilterCustomer)
        {
            using (FestiSpecEntities context = new FestiSpecEntities())
            {
                return context.Klant.Include("Contactpersoon").Where(e => e.Naam.Contains(FilterCustomer)).OrderByDescending(e => e.Naam).ToList();
            }
        } 
        #endregion

    }
}
