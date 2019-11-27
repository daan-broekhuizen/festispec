namespace Festispec.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Antwoorden")]
    public partial class Antwoorden
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VIC_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int InspecteurID { get; set; }

        public int AntwoordNummer { get; set; }

        [Column("Antwoord_text", TypeName = "text")]
        public string AntwoordText { get; set; }

        [Column("Antwoord_image", TypeName = "image")]
        public byte[] AntwoordImage { get; set; }

        [Column("Laatste_wijziging")]
        public DateTime LaatsteWijziging { get; set; }

        public virtual Account Account { get; set; }

        public virtual InspectieformulierVragenlijstCombinatie InspectieformulierVragenlijstCombinatie { get; set; }
    }
}
