using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Model.Repositories
{
    public interface ICustomerRepository
    {
        List<Klant> GetCustomers();
        void UpdateCustomer(Klant klant);
        Klant CreateCustomer(Klant klant);
        void UpdateContactPerson(Contactpersoon contact);
        Contactpersoon CreateContactPerson(Contactpersoon contactpersoon);
    }
}
