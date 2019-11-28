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
            Beschikbaarheid_inspecteurs = new HashSet<Beschikbaarheid_inspecteurs>();
            Opdracht = new HashSet<Opdracht>();
            Opdracht1 = new HashSet<Opdracht>();
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

        [StringLength(50)]
        public string Stad { get; set; }

        [StringLength(50)]
        public string Straatnaam { get; set; }

        [StringLength(4)]
        public string Huisnummer { get; set; }

        [StringLength(120)]
        public string Email { get; set; }

        [StringLength(10)]
        public string Telefoonnummer { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Datum_certificering { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Einddatum_certificering { get; set; }

        [StringLength(18)]
        public string IBAN { get; set; }

        public DateTime Laatste_wijziging { get; set; }

        public virtual Rol_lookup Rol_lookup { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Antwoorden> Antwoorden { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Beschikbaarheid_inspecteurs> Beschikbaarheid_inspecteurs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Opdracht> Opdracht { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Opdracht> Opdracht1 { get; set; }
    }
}
