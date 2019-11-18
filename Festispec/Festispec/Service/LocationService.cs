using BingMapsRESTToolkit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Service
{
    public class LocationService
    {
        public async Task<Address> GetFullAdress(string address)
        {
            GeocodeRequest req = new GeocodeRequest()
            {
                BingMapsKey = "AjUhVNG2ZqZGCCTbumwOX2Z4c2bGI3LwaqaMUz7WiRgxGWtv8VuE9X7Va89MQ7SU",
                Query = "NL " + address
            };

            Response response = await req.Execute();
            Location location = response.ResourceSets[0].Resources[0] as Location;
            return location.Address;
        }
    }
}
