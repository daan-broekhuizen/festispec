namespace Festispec.WebApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Vraag")]
    public partial class Vraag
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Vraag()
        {
            Antwoorden = new HashSet<Antwoorden>();
            Vraag_mogelijk_antwoord = new HashSet<Vraag_mogelijk_antwoord>();
        }

        public int VraagID { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Vraagstelling { get; set; }

        public int VolgordeNummer { get; set; }

        public int InspectieFormulierID { get; set; }

        [Required]
        [StringLength(2)]
        public string Vraagtype { get; set; }

        [StringLength(150)]
        public string AfbeeldingURL { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Antwoorden> Antwoorden { get; set; }

        public virtual Inspectieformulier Inspectieformulier { get; set; }

        public virtual Vraagtype_lookup Vraagtype_lookup { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vraag_mogelijk_antwoord> Vraag_mogelijk_antwoord { get; set; }
    }
}
