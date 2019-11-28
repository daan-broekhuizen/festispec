namespace Festispec.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Inspectieformulier_vragenlijst_combinatie")]
    public partial class InspectieformulierVragenlijstCombinatie
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InspectieformulierVragenlijstCombinatie()
        {
            Antwoorden = new HashSet<Antwoorden>();
        }

        [Key]
        public int VIC_ID { get; set; }

        public int InspectieformulierID { get; set; }

        public int VraagID { get; set; }

        public int VraagVolgordeNummer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Antwoorden> Antwoorden { get; set; }

        public virtual Inspectieformulier Inspectieformulier { get; set; }

        public virtual Vraag Vraag { get; set; }
    }
}
