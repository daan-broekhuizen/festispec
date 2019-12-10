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
            VraagMogelijkAntwoord = new HashSet<VraagMogelijkAntwoord>();
            Antwoorden = new HashSet<Antwoorden>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int VraagID { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Vraagstelling { get; set; }

        [Required]
        public int VolgordeNummer { get; set; }

        [Required]
        public int InspectieFormulierID { get; set; }

        [Required]
        [StringLength(2)]
        public string Vraagtype { get; set; }

        [StringLength(150)]
        public string AfbeeldingURL { get; set; }

        public virtual VraagType VraagtypeLookup { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VraagMogelijkAntwoord> VraagMogelijkAntwoord { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Antwoorden> Antwoorden { get; set; }
    }
}
