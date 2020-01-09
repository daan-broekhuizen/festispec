using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Model.DTO
{
    public class JsonJob
    {
        public int ID { get; set; }
        public string KlantNaam { get; set; }
        public string OpdrachtNaam { get; set; }
        public string KlantWensen { get; set; }
        public string Status { get; set; }
        public DateTime StartDatum { get; set; }
        public DateTime EindDatum { get; set; }
        public byte[] Logo { get; set; }

        public static explicit operator JsonJob(Opdracht opdracht)
        {
            return new JsonJob
            {
                ID = opdracht.OpdrachtID,
                KlantNaam = opdracht.Klant.Naam,
                OpdrachtNaam = opdracht.OpdrachtNaam,
                KlantWensen = opdracht.Klantwensen,
                Status = opdracht.StatusLookup.Betekenis,
                StartDatum = opdracht.StartDatum,
                EindDatum = opdracht.EindDatum,
                Logo = opdracht.Klant.KlantLogo
            };
        }

    }
}
