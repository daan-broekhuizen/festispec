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
            Inspectieformulier_vragenlijst_combinatie = new HashSet<Inspectieformulier_vragenlijst_combinatie>();
            Vraag_mogelijk_antwoord = new HashSet<Vraag_mogelijk_antwoord>();
        }

        public int VraagID { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Vraagstelling { get; set; }

        [Required]
        [StringLength(2)]
        public string Vraagtype { get; set; }

        [Column(TypeName = "image")]
        public byte[] Bijlage { get; set; }

        public DateTime Laatste_wijziging { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Inspectieformulier_vragenlijst_combinatie> Inspectieformulier_vragenlijst_combinatie { get; set; }

        public virtual Vraagtype_lookup Vraagtype_lookup { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vraag_mogelijk_antwoord> Vraag_mogelijk_antwoord { get; set; }
    }
}
