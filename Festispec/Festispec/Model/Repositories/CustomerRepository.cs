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
                Klant toUpdate = context.Klant.Where(c => c.KlantID == klant.KlantID).FirstOrDefault();
                context.Entry(toUpdate).CurrentValues.SetValues(klant);
                context.SaveChanges();
            }
        }

        public Klant CreateCustomer(Klant klant)
        {
            using (FestispecContext context = new FestispecContext())
            {
                Klant customer = context.Klant.Add(klant);
                context.SaveChanges();
                return customer;
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

        public Contactpersoon CreateContactPerson(Contactpersoon contactpersoon)
        {
            using (FestispecContext context = new FestispecContext())
            {
                Contactpersoon contact = context.Contactpersoon.Add(contactpersoon);
                context.SaveChanges();
                return contact;
            }
        }
    }
}
