DROP PROCEDURE GetInspectors;

CREATE PROCEDURE GetInspectors @ID int
AS
SELECT Account.AccountID, Account.Gebruikersnaam, Account.Wachtwoord, Account.Rol, Account.Voornaam, Account.Tussenvoegsel, Account.Achternaam, Account.Stad, Account.Straatnaam, Account.Huisnummer, Account.Email, Account.Telefoonnummer, Account.Datum_certificering as Datumcertificering, Account.Einddatum_certificering as EinddatumCertificering, Account.IBAN, Account.Laatste_wijziging as LaatsteWijziging FROM  Account 
INNER JOIN Beschikbaarheid_inspecteurs
ON Account.AccountID = Beschikbaarheid_inspecteurs.MedewerkerID 
INNER JOIN Inspectieformulier
ON
Beschikbaarheid_inspecteurs.Datum = Inspectieformulier.Datum_Inspectie 
WHERE Beschikbaarheid_inspecteurs.Datum = Inspectieformulier.Datum_Inspectie
AND Inspectieformulier.InspectieformulierID = @ID


EXEC GetInspectors @ID = 1;