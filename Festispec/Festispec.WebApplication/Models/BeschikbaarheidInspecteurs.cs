namespace Festispec.WebApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Beschikbaarheid_inspecteurs")]
    public partial class BeschikbaarheidInspecteurs
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MedewerkerID { get; set; }

        [Key]
        [Column(Order = 1, TypeName = "date")]
        public DateTime Datum { get; set; }

        public virtual Account Account { get; set; }
    }
}
