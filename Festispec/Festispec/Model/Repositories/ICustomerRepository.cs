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
        void CreateCustomer(Klant klant);
        void UpdateContactPerson(Contactpersoon contact);
        void CreateContactPerson(Contactpersoon contactpersoon);

    }
}
