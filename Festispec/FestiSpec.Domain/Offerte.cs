
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
    
public partial class Offerte
{

    public int OfferteID { get; set; }

    public int OpdrachtID { get; set; }

    public Nullable<decimal> Totaalbedrag { get; set; }

    public System.DateTime Aanmaakdatum { get; set; }

    public string Beschrijving { get; set; }

    public string Klantbeslissing_reden { get; set; }

    public System.DateTime Laatste_weiziging { get; set; }



    public virtual Opdracht Opdracht { get; set; }

}

}
