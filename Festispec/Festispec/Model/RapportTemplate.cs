namespace Festispec.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Rapport_template")]
    public partial class RapportTemplate
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RapportTemplate()
        {
            Opdracht = new HashSet<Opdracht>();
        }

        [Key]
        public int TemplateID { get; set; }

        [Column(TypeName = "text")]
        public string TemplateText { get; set; }

        [MaxLength(50)]
        public string TemplateName { get; set; }

        [MaxLength(400)]
        public string TemplateDescription { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Opdracht> Opdracht { get; set; }
    }
}
