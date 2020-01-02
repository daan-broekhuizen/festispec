namespace Festispec.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Klant")]
    public partial class Klant
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Klant()
        {
            Contactpersoon = new HashSet<Contactpersoon>();
            Opdracht = new HashSet<Opdracht>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int KlantID { get; set; }

        [StringLength(8)]
        [Column("KvK_nummer")]
        public string KvKNummer { get; set; }

        [StringLength(8)]
        public string Vestigingnummer { get; set; }


        [Required]
        [StringLength(45)]
        public string Naam { get; set; }

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
        [StringLength(20)]
        public string Telefoonnummer { get; set; }

        [Required]
        [StringLength(130)]
        public string Email { get; set; }

        [StringLength(100)]
        public string Website { get; set; }

        [Column("Klant_logo", TypeName = "image")]
        public byte[] KlantLogo { get; set; }

        [Column("Laatste_wijziging")]
        public DateTime LaatsteWijziging { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Contactpersoon> Contactpersoon { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Opdracht> Opdracht { get; set; }
    }
}
