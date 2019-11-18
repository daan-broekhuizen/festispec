namespace Festispec.Model
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

        [Column("Klantbeslissing_reden", TypeName = "text")]
        public string KlantbeslissingReden { get; set; }

        [Column("Laatste_wijziging")]
        public DateTime LaatsteWijziging { get; set; }

        public virtual Opdracht Opdracht { get; set; }
    }
}
