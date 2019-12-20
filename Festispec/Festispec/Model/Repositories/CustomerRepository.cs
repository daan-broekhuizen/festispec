using Festispec.Model;
using Festispec.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestiSpec.Domain.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public List<Klant> GetCustomers()
        {
            using(FestispecContext context = new FestispecContext())
            {
                return context.Klant.Include("Contactpersoon").ToList();
            }
        }

        public void UpdateCustomer(Klant klant)
        {
            using(FestispecContext context = new FestispecContext())
            {
                Klant toUpdate = context.Klant.Where(c => c.KvKNummer == klant.KvKNummer).FirstOrDefault();
                context.Entry(toUpdate).CurrentValues.SetValues(klant);
                context.SaveChanges();
            }
        }

        public void CreateCustomer(Klant klant)
        {
            using (FestispecContext context = new FestispecContext())
            {
                context.Klant.Add(klant);
                context.SaveChanges();
            }
        }

        public void UpdateContactPerson(Contactpersoon contact)
        {
            using (FestispecContext context = new FestispecContext())
            {
                Contactpersoon toUpdate = context.Contactpersoon.Where(c => c.ContactpersoonID == contact.ContactpersoonID).FirstOrDefault();
                context.Entry(toUpdate).CurrentValues.SetValues(contact);
                context.SaveChanges();
            }
        }

        public void CreateContactPerson(Contactpersoon contactpersoon)
        {
            using (FestispecContext context = new FestispecContext())
            {
                Klant klant = context.Klant.Where(k => k.KlantID == contactpersoon.KlantID).FirstOrDefault();
                klant.Contactpersoon.Add(contactpersoon);
                context.SaveChanges();
            }
        }
    }
}
