namespace Festispec.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSP : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure("sp_GetInspectors", p => new { ID = p.Int() },
                @"BEGIN
            SELECT Account.AccountID, Account.Gebruikersnaam, Account.Wachtwoord, Account.Rol, Account.Voornaam, Account.Tussenvoegsel, Account.Achternaam, Account.Stad, Account.Straatnaam, Account.Huisnummer, Account.Email, Account.Telefoonnummer, Account.Datum_certificering as Datumcertificering, Account.Einddatum_certificering as EinddatumCertificering, Account.IBAN, Account.Laatste_wijziging as LaatsteWijziging FROM  Account 
            INNER JOIN Beschikbaarheid_inspecteurs
            ON Account.AccountID = Beschikbaarheid_inspecteurs.MedewerkerID 
            INNER JOIN Inspectieformulier
            ON
            Beschikbaarheid_inspecteurs.Datum = Inspectieformulier.Datum_Inspectie 
            WHERE Beschikbaarheid_inspecteurs.Datum = Inspectieformulier.Datum_Inspectie
            AND Inspectieformulier.InspectieformulierID = @ID;

            END;"
            );
        }

        public void Down() => DropStoredProcedure("sp_GetInspectors");
    }
}
