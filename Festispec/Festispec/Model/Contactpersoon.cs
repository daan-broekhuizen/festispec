namespace Festispec.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Contactpersoon")]
    public partial class Contactpersoon
    {
        public int ContactpersoonID { get; set; }

        [Required]
        public int KlantID { get; set; }

        [Required]
        [StringLength(30)]
        public string Voornaam { get; set; }

        [StringLength(15)]
        public string Tussenvoegsel { get; set; }

        [Required]
        [StringLength(30)]
        public string Achternaam { get; set; }

        [Required]
        [StringLength(130)]
        public string Email { get; set; }

        [Required]
        [StringLength(10)]
        public string Telefoon { get; set; }

        [Column(TypeName = "text")]
        public string Notities { get; set; }

        [Column("Laatste_wijziging")]
        public DateTime LaatsteWijziging { get; set; }

        [StringLength(30)]
        [Column("Rol")]
        public string Rol { get; set; }

        public virtual Klant Klant { get; set; }
    }
}
