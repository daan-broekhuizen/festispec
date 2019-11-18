namespace Festispec.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Beschikbaarheid_inspecteurs
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
