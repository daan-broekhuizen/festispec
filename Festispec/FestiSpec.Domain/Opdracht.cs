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
    
    public partial class Opdracht
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Opdracht()
        {
            this.Inspectieformulier = new HashSet<Inspectieformulier>();
            this.Offerte = new HashSet<Offerte>();
            this.Account1 = new HashSet<Account>();
        }
    
        public int OpdrachtID { get; set; }
        public string Opdracht_naam { get; set; }
        public string Status { get; set; }
        public System.DateTime Creatie_datum { get; set; }
        public int KlantID { get; set; }
        public int MedewerkerID { get; set; }
        public string Gebruikte_rechtsgebieden { get; set; }
        public string Rapportage { get; set; }
        public string Advies { get; set; }
        public System.DateTime Laatste_weiziging { get; set; }
    
        public virtual Account Account { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Inspectieformulier> Inspectieformulier { get; set; }
        public virtual Klant Klant { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Offerte> Offerte { get; set; }
        public virtual Status_lookup Status_lookup { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Account> Account1 { get; set; }
    }
}
