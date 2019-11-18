namespace Festispec.Model
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
            InspectieformulierVragenlijstCombinatie = new HashSet<InspectieformulierVragenlijstCombinatie>();
        }

        public int InspectieformulierID { get; set; }

        [Required]
        [StringLength(45)]
        public string InspectieFormulierTitel { get; set; }

        [Column("Datum_inspectie", TypeName = "date")]
        public DateTime? DatumInspectie { get; set; }

        [Column(TypeName = "text")]
        public string Locatie { get; set; }

        public int? OpdrachtID { get; set; }

        [Column(TypeName = "text")]
        public string Beschrijving { get; set; }

        [Column("Laatste_wijziging")]
        public DateTime LaatsteWijziging { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InspectieformulierVragenlijstCombinatie> InspectieformulierVragenlijstCombinatie { get; set; }

        public virtual Opdracht Opdracht { get; set; }
    }
}
