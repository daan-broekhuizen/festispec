namespace Festispec.WebApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Opdracht")]
    public partial class Opdracht
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Opdracht()
        {
            Inspectieformulier = new HashSet<Inspectieformulier>();
            Offerte = new HashSet<Offerte>();
        }

        public int OpdrachtID { get; set; }

        [Required]
        [StringLength(45)]
        [Column("Opdracht_naam")]
        public string OpdrachtNaam { get; set; }

        [Required]
        [StringLength(30)]
        public string Status { get; set; }

        [Column("Creatie_datum", TypeName = "date")]
        public DateTime CreatieDatum { get; set; }

        [Column("Start_datum", TypeName = "date")]
        public DateTime StartDatum { get; set; }

        [Column("Eind_datum", TypeName = "date")]
        public DateTime EindDatum { get; set; }

        [Required]
        [StringLength(8)]
        public string KlantID { get; set; }

        public int MedewerkerID { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Klantwensen { get; set; }

        [Column("Gebruikte_rechtsgebieden", TypeName = "text")]
        public string GebruikteRechtsgebieden { get; set; }

        [Column(TypeName = "text")]
        public string Rapportage { get; set; }

        [Column("Rapportage_uses_template")]
        public int? RapportageUsesTemplate { get; set; }

        [Column("Laatste_wijziging")]
        public DateTime LaatsteWijziging { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Inspectieformulier> Inspectieformulier { get; set; }

        public virtual Klant Klant { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Offerte> Offerte { get; set; }

        public virtual RapportTemplate RapportTemplate { get; set; }

        public virtual Status StatusLookup { get; set; }
    }
}
