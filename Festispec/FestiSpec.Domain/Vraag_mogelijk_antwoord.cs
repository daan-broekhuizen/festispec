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
    
    public partial class Vraag_mogelijk_antwoord
    {
        public int VraagID { get; set; }
        public int Antwoord_nummer { get; set; }
        public string Antwoord_text { get; set; }
    
        public virtual Vraag Vraag { get; set; }
    }
}