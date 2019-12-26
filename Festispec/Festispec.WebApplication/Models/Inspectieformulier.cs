namespace Festispec.WebApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Inspectieformulier")]
    public partial class Inspectieformulier
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Inspectieformulier()
        {
            Vraag = new HashSet<Vraag>();
            Ingepland = new HashSet<Account>();
        }

        public int InspectieformulierID { get; set; }

        [Required]
        [StringLength(45)]
        public string InspectieFormulierTitel { get; set; }

        [Column("Datum_inspectie", TypeName = "date")]
        public DateTime? DatumInspectie { get; set; }

        public TimeSpan? StartTijd { get; set; }

        public TimeSpan? EindTijd { get; set; }

        [StringLength(50)]
        public string Stad { get; set; }

        [StringLength(50)]
        public string Straatnaam { get; set; }

        [StringLength(4)]
        public string Huisnummer { get; set; }

        public int? OpdrachtID { get; set; }

        [Column(TypeName = "text")]
        public string Beschrijving { get; set; }

        [Column("Laatste_wijziging")]
        public DateTime LaatsteWijziging { get; set; }
        [Column("Benodigde_Inspecteurs")]
        public int? BenodigdeInspecteurs { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vraag> Vraag { get; set; }

        public virtual Opdracht Opdracht { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Account> Ingepland { get; set; }
    }
}
