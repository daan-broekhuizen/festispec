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
        private static string _bingApiKey = "AjUhVNG2ZqZGCCTbumwOX2Z4c2bGI3LwaqaMUz7WiRgxGWtv8VuE9X7Va89MQ7SU";
        public async Task<Address> GetFullAdress(string address)
        {
            GeocodeRequest req = new GeocodeRequest()
            {
                BingMapsKey = _bingApiKey,
                Query = "NL " + address
            };

            Response response = await req.Execute();
            Location location = response.ResourceSets[0].Resources[0] as Location;
            return location.Address;
        }

        public async Task<double> CalculateDistance(string origin, string destination)
        {
            DistanceMatrixRequest req = new DistanceMatrixRequest()
            {
                BingMapsKey = _bingApiKey,
                Origins = new List<SimpleWaypoint>()
                {
                    new SimpleWaypoint(origin)
                },
                Destinations = new List<SimpleWaypoint>()
                {
                    new SimpleWaypoint(destination)
                },
                DistanceUnits = DistanceUnitType.Kilometers,
                TravelMode = TravelModeType.Driving
            };

            Response response = await req.Execute();
            Resource resource = response.ResourceSets[0].Resources[0];
            DistanceMatrix matrix = resource as DistanceMatrix;
            DistanceMatrixCell cell = matrix.Results[0];

            return cell.TravelDistance;
        }
    }
}
