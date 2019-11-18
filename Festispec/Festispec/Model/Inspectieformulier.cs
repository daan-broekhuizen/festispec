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
            Inspectieformulier_vragenlijst_combinatie = new HashSet<Inspectieformulier_vragenlijst_combinatie>();
        }

        public int InspectieformulierID { get; set; }

        [Required]
        [StringLength(45)]
        public string InspectieFormulierTitel { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Datum_inspectie { get; set; }

        [Column(TypeName = "text")]
        public string Locatie { get; set; }

        public int? OpdrachtID { get; set; }

        [Column(TypeName = "text")]
        public string Beschrijving { get; set; }

        public DateTime Laatste_weiziging { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Inspectieformulier_vragenlijst_combinatie> Inspectieformulier_vragenlijst_combinatie { get; set; }

        public virtual Opdracht Opdracht { get; set; }
    }
}
