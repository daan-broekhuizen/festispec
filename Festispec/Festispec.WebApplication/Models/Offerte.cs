namespace Festispec.WebApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Offerte")]
    public partial class Offerte
    {
        public int OfferteID { get; set; }

        public int OpdrachtID { get; set; }

        public decimal? Totaalbedrag { get; set; }

        [Column(TypeName = "date")]
        public DateTime Aanmaakdatum { get; set; }

        [Column(TypeName = "text")]
        public string Beschrijving { get; set; }

        [Column(TypeName = "text")]
        public string Klantbeslissing_reden { get; set; }

        public DateTime Laatste_wijziging { get; set; }

        public virtual Opdracht Opdracht { get; set; }
    }
}
