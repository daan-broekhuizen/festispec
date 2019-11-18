namespace Festispec.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Inspectieformulier_vragenlijst_combinatie
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Inspectieformulier_vragenlijst_combinatie()
        {
            Antwoorden = new HashSet<Antwoorden>();
        }

        [Key]
        public int VIC_ID { get; set; }

        public int InspectieformulierID { get; set; }

        public int VraagID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Antwoorden> Antwoorden { get; set; }

        public virtual Inspectieformulier Inspectieformulier { get; set; }

        public virtual Vraag Vraag { get; set; }
    }
}
