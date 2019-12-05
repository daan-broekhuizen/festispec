using Festispec.Model;
using Festispec.Model.Repositories;
using Festispec.Service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel
{
    public class PlanningViewModel
    {
        private void Swap(ref double valOne, ref double valTwo)
        {
            valOne = valOne + valTwo;
            valTwo = valOne - valTwo;
            valOne = valOne - valTwo;
        }

        private double[] BubbleSort(double[] inputArray)
        {
            for (int iterator = 0; iterator < inputArray.Length; iterator++)
            {
                for (int index = 0; index < inputArray.Length - 1; index++)
                {
                    if (inputArray[index] > inputArray[index + 1])
                    {
                        Swap(ref inputArray[index], ref inputArray[index + 1]);
                    }
                }
            }

            return inputArray;
        }

        public async Task<List<Account>> GetInspectorAsync(int id, string destination, int inspectorNeeded)
        {
            //TODO: Error als je niet genoeg inspectorren hebben
            //TODO: Zorgen dat hij de inspecteur pakt die het dichste bij is
            //TODO Miss even een list met LINQ implementeren

            PlanningRepository repo = new PlanningRepository();
            LocationService service = new LocationService();

            List<Account> accounts = repo.GetFreeInspectors(id);
            List<Account> inspectors = new List<Account>();
            string[] tempInspectors = new string[inspectorNeeded];
            double[] distances = new double[accounts.Count];

            string[] data = new string[accounts.Count];

            // Als er te weinig Inspectors zijn
            if (accounts.Count < inspectorNeeded)
                return null;

            // Hier word de afstand berekend
            for (int i = 0; i < accounts.Count; i++)
            {
                string address = accounts[i].Straatnaam + " " + accounts[i].Huisnummer + " " + accounts[i].Stad;
                distances[i]  = await service.CalculateDistance(address, destination);
                data[i] = accounts[i].Gebruikersnaam + " " + distances[i];
                Console.WriteLine(data[i]);
                
            }

            // NU KOMT HET HOOR
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


            // TEST LOOP
            for (int i1 = 0; i1 < inspectors.Count; i1++)
            {
                Console.WriteLine(inspectors[i1].Gebruikersnaam);
            }

            return inspectors;
        }
    }
}
