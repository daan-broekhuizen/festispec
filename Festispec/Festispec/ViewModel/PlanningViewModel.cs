using Festispec.Model;
using Festispec.Model.Repositories;
using Festispec.Service;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Festispec.ViewModel
{
    public class PlanningViewModel
    {
        /// <summary>
        /// Maakt een planning aan voor alle beschikbare inspecteurs
        /// </summary>
        /// <param name="id">Het ID van het festival</param>
        /// <param name="destination">De bestemming van het festival</param>
        /// <param name="inspectorNeeded">Hoeveelheid benodigde inspectoren</param>
        /// <returns></returns>
        public async Task<List<Account>> GetInspectorAsync(int id, string destination, int inspectorNeeded)
        {
            PlanningRepository repo = new PlanningRepository();
            LocationService service = new LocationService();

            List<Account> accounts = repo.GetFreeInspectors(id);
            List<Account> inspectors = new List<Account>();
            string[] tempInspectors = new string[inspectorNeeded];
            double[] distances = new double[accounts.Count];

            string[] data = new string[accounts.Count];

            // Role check
            for (int i = 0; i < accounts.Count; i++)
            {
                if (accounts[i].Rol != "in")
                    accounts.Remove(accounts[i]);
            }

            // Als er te weinig Inspectors zijn
            if (accounts.Count < inspectorNeeded)
                return null;

            // Hier word de afstand berekend
            for (int i = 0; i < accounts.Count; i++)
            {
                string address = accounts[i].Straatnaam + " " + accounts[i].Huisnummer + " " + accounts[i].Stad;
                distances[i]  = await service.CalculateDistance(address, destination);
                data[i] = accounts[i].Gebruikersnaam + " " + distances[i];               
            }

            distances = distances.OrderBy(d => d).ToArray();

            for (int i = 0; i < inspectorNeeded; i++)
            {
                double tmp = distances[i];

                for (int y = 0; y < accounts.Count; y++)
                {
                    if (data[y].Contains(tmp.ToString()))
                        tempInspectors[i] = data[y];
                }

                for(int x = 0; x < accounts.Count; x++)
                {
                    if (tempInspectors[i].Contains(accounts[x].Gebruikersnaam))
                        inspectors.Add(accounts[i]);
                }
            }

            // Alles toevoegen aan de ingeplande inspecteurs YA GET MEE
            foreach (Account result in inspectors)
                repo.AddToPlanning(result.AccountID, id);
            

            return inspectors;
        }
    }
}
