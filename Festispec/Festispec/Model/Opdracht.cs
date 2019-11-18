namespace Festispec.Model
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
            Account1 = new HashSet<Account>();
        }

        public int OpdrachtID { get; set; }

        [Required]
        [StringLength(45)]
        public string Opdracht_naam { get; set; }

        [Required]
        [StringLength(2)]
        public string Status { get; set; }

        [Column(TypeName = "date")]
        public DateTime Creatie_datum { get; set; }

        [Required]
        [StringLength(8)]
        public string KlantID { get; set; }

        public int MedewerkerID { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Klantwensen { get; set; }

        [Column(TypeName = "text")]
        public string Gebruikte_rechtsgebieden { get; set; }

        [Column(TypeName = "text")]
        public string Rapportage { get; set; }

        public int? Rapportage_uses_template { get; set; }

        public DateTime Laatste_weiziging { get; set; }

        public virtual Account Account { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Inspectieformulier> Inspectieformulier { get; set; }

        public virtual Klant Klant { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Offerte> Offerte { get; set; }

        public virtual Rapport_template Rapport_template { get; set; }

        public virtual Status_lookup Status_lookup { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Account> Account1 { get; set; }
    }
}
