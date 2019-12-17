namespace Festispec.WebApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Vraagtype_lookup")]
    public partial class VraagType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VraagType()
        {
            Vraag = new HashSet<Vraag>();
        }

        [Key]
        [StringLength(2)]
        public string Afkorting { get; set; }

        [Required]
        [StringLength(30)]
        public string Beschrijving { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Vraag> Vraag { get; set; }
    }
}
