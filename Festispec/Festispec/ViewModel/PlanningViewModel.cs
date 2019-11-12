using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BingMapsRESTToolkit;
using FestiSpec.Domain.Repositories;

namespace Festispec.ViewModel
{
    class PlanningViewModel
    {
        public async Task CalculateDistance()
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

            Response res = await req.Execute();
            Resource r = res.ResourceSets[0].Resources[0];
            DistanceMatrix m = r as DistanceMatrix;
            DistanceMatrixCell c = m.Results[0];
            Debug.WriteLine("Afstand: " + c.TravelDistance.ToString("n1") + " km");

        }
    }
}
