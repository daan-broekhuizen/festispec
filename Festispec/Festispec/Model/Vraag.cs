namespace Festispec.Model
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
            InspectieformulierVragenlijstCombinatie = new HashSet<InspectieformulierVragenlijstCombinatie>();
            VraagMogelijkAntwoord = new HashSet<VraagMogelijkAntwoord>();
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

        [Column("Laatste_wijziging")]
        public DateTime LaatsteWijziging { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InspectieformulierVragenlijstCombinatie> InspectieformulierVragenlijstCombinatie { get; set; }

        public virtual VraagType VraagtypeLookup { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VraagMogelijkAntwoord> VraagMogelijkAntwoord { get; set; }
    }
}
