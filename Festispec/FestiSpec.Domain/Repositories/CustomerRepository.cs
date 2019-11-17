﻿using System;
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
                Klant toUpdate = context.Klant.Where(c => c.KvK_nummer == klant.KvK_nummer).FirstOrDefault();
                context.Entry(toUpdate).CurrentValues.SetValues(klant);
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

        public void UpdateContactPerson(Contactpersoon contact)
        {
            using (FestiSpecEntities context = new FestiSpecEntities())
            {
                Klant toUpdate = context.Klant.Where(c => c.KvK_nummer == contact.KlantID).FirstOrDefault();
                context.Entry(toUpdate).CurrentValues.SetValues(contact);
                context.SaveChanges();
            }
        }

        public void CreateContactPerson(Contactpersoon contactpersoon)
        {
            using (FestiSpecEntities context = new FestiSpecEntities())
            {
                Klant klant = context.Klant.Where(k => k.KvK_nummer == contactpersoon.KlantID).FirstOrDefault();
                klant.Contactpersoon.Add(contactpersoon);
                context.SaveChanges();
            }
        }

    }
}