namespace Festispec.WebApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Account")]
    public partial class Account
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Account()
        {
            Antwoorden = new HashSet<Antwoorden>();
            BeschikbaarheidInspecteurs = new HashSet<BeschikbaarheidInspecteurs>();
            Ingepland = new HashSet<Inspectieformulier>();
        }

        public int AccountID { get; set; }

        [Required]
        [StringLength(45)]
        public string Gebruikersnaam { get; set; }

        [Required]
        [StringLength(45)]
        public string Wachtwoord { get; set; }

        [Required]
        [StringLength(2)]
        public string Rol { get; set; }

        [Required]
        [StringLength(30)]
        public string Voornaam { get; set; }

        [StringLength(15)]
        public string Tussenvoegsel { get; set; }

        [Required]
        [StringLength(30)]
        public string Achternaam { get; set; }

        [Required]
        [StringLength(50)]
        public string Stad { get; set; }

        [Required]
        [StringLength(50)]
        public string Straatnaam { get; set; }

        [Required]
        [StringLength(4)]
        public string Huisnummer { get; set; }

        [Required]
        [StringLength(120)]
        public string Email { get; set; }

        [Required]
        [StringLength(10)]
        public string Telefoonnummer { get; set; }

        [Column("Datum_certificering", TypeName = "date")]
        public DateTime? DatumCertificering { get; set; }

        [Column("Einddatum_certificering", TypeName = "date")]
        public DateTime? EinddatumCertificering { get; set; }

        [Required]
        [StringLength(18)]
        public string IBAN { get; set; }

        [Column("Laatste_wijziging")]
        public DateTime LaatsteWijziging { get; set; }

        public virtual Rol RolType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Antwoorden> Antwoorden { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BeschikbaarheidInspecteurs> BeschikbaarheidInspecteurs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Inspectieformulier> Ingepland { get; set; }
    }
}
