using Festispec.Model.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Model.Repositories
{
    public class GraphRepository
    {
        #region Sales
        public List<SaleModel> GetAllSales()
        {
            List<SaleModel> sales = new List<SaleModel>();

            Random rand = new Random();
            foreach (EnumMonth month in Enum.GetValues(typeof(EnumMonth)))
            {
                int entries = rand.Next(1, 10);
                for (int i = 0; i < entries; i++)
                    sales.Add(new SaleModel(month, "1000AA"));
            }

            return sales;
        }

        public int GetSaleAmountForMonth(EnumMonth month)
        {
            return GetAllSales().Where(x => x.Month == month).Count();
        }

        public double[] GetSaleValues()
        {
            double[] salesValues = new double[12];

            Array months = Enum.GetValues(typeof(EnumMonth));
            for(int i = 0; i < months.Length; i++)
            {
                salesValues[i] = GetSaleAmountForMonth((EnumMonth)months.GetValue(i));
            }

            return salesValues;
        }
        #endregion

        #region Inspector
        public List<InspectorModel> GetAllInspectors()
        {
            List<InspectorModel> inspectors = new List<InspectorModel>();

            Random rand = new Random();
            foreach (EnumMonth month in Enum.GetValues(typeof(EnumMonth)))
            {
                int entries = rand.Next(1, 10);
                for (int i = 0; i < entries; i++)
                    inspectors.Add(new InspectorModel("Test", month));
            }

            return inspectors;
        }

        public int GetInspectorAmountForMonth(EnumMonth month)
        {
            return GetAllInspectors().Where(x => x.RegistrationMonth == month).Count();
        }

        public double[] GetInspectorValues()
        {
            double[] inspectorValues = new double[12];

            Array months = Enum.GetValues(typeof(EnumMonth));
            for (int i = 0; i < months.Length; i++)
            {
                inspectorValues[i] = GetInspectorAmountForMonth((EnumMonth)months.GetValue(i));
            }

            return inspectorValues;
        }
        #endregion

        #region Ofertes
        public List<OfferteModel> GetAllOffertes()
        {
            List<OfferteModel> offertes = new List<OfferteModel>();

            Random rand = new Random();
            foreach (EnumOfferteStatus status in Enum.GetValues(typeof(EnumOfferteStatus)))
            {
                int entries = rand.Next(1, 10);
                for (int i = 0; i < entries; i++)
                    offertes.Add(new OfferteModel("Test", status));
            }

            return offertes;
        }


        public int GetOffertesAmountByStatus(EnumOfferteStatus status)
        {
            return GetAllOffertes().Where(x => x.Status == status).Count();
        }
        #endregion
    }
}