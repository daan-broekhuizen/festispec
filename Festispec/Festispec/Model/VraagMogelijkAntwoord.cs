namespace Festispec.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Vraag_mogelijk_antwoord")]
    public partial class VraagMogelijkAntwoord
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VraagID { get; set; }

        [Key]
        [Column("Antwoord_nummer", Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AntwoordNummer { get; set; }

        [Column("Antwoord_text", TypeName = "text")]
        [Required]
        public string AntwoordText { get; set; }

        public virtual Vraag Vraag { get; set; }
    }
}
