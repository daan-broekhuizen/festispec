using BingMapsRESTToolkit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Service
{
    public class PostcodeService
    {
        public async Task<Address> GetAdress(string postcode)
        {
            SimpleAddress address = new SimpleAddress()
            {
                Locality = "breda",
                CountryRegion = "netherlands",
                AddressLine = "rozenlaan 35"
            };

            GeocodeRequest req = new GeocodeRequest()
            {
                BingMapsKey = "AjUhVNG2ZqZGCCTbumwOX2Z4c2bGI3LwaqaMUz7WiRgxGWtv8VuE9X7Va89MQ7SU",
                IncludeNeighborhood = true,
                Address = address
            };

            Response response = await req.Execute();
            Location location = response.ResourceSets[0].Resources[0] as Location;
            Console.WriteLine(location.Address.PostalCode);
            return location.Address;
        }
    }
}
