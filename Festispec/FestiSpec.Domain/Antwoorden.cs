//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FestiSpec.Domain
{
    using System;
    using System.Collections.Generic;
    
    public partial class Antwoorden
    {
        public int VIC_ID { get; set; }
        public int InspecteurID { get; set; }
        public string Antwoord_text { get; set; }
        public byte[] Antwoord_image { get; set; }
        public System.DateTime Laatste_weiziging { get; set; }
    
        public virtual Account Account { get; set; }
        public virtual Inspectieformulier_vragenlijst_combinatie Inspectieformulier_vragenlijst_combinatie { get; set; }
    }
}
