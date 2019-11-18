using BingMapsRESTToolkit;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Festispec.ViewModel
{
    class PlanningViewModel
    {
        public async Task<double> CalculateDistance()
        {
            DistanceMatrixRequest req = new DistanceMatrixRequest()
            {
                BingMapsKey = "AjUhVNG2ZqZGCCTbumwOX2Z4c2bGI3LwaqaMUz7WiRgxGWtv8VuE9X7Va89MQ7SU",
                Origins = new List<SimpleWaypoint>()
                {
                    new SimpleWaypoint("Efteling, 5171 SL Kaatsheuvel")
                },
                Destinations = new List<SimpleWaypoint>()
                {
                    new SimpleWaypoint("Griekenland")
                },
                DistanceUnits = DistanceUnitType.Kilometers,
                TravelMode = TravelModeType.Driving
            };

            Response response = await req.Execute();
            Resource resource = response.ResourceSets[0].Resources[0];
            DistanceMatrix matrix = resource as DistanceMatrix;
            DistanceMatrixCell cell = matrix.Results[0];
            Debug.WriteLine("Afstand: " + cell.TravelDistance.ToString("n1") + " km");

            return cell.TravelDistance;
        }
    }
}