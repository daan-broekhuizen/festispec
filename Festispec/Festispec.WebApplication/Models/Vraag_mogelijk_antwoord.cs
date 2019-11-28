namespace Festispec.WebApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Vraag_mogelijk_antwoord
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VraagID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Antwoord_nummer { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Antwoord_text { get; set; }

        public virtual Vraag Vraag { get; set; }
    }
}
