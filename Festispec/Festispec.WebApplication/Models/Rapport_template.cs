namespace Festispec.WebApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Rapport_template
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Rapport_template()
        {
            Opdracht = new HashSet<Opdracht>();
        }

        [Key]
        public int TemplateID { get; set; }

        [Column(TypeName = "text")]
        public string TemplateText { get; set; }

        [StringLength(50)]
        public string TemplateName { get; set; }

        [StringLength(400)]
        public string TemplateDescription { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Opdracht> Opdracht { get; set; }
    }
}