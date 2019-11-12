MERGE INTO dbo.Account AS Target
USING (Values
	('HansKlok', 'ab123', 'in', 'Hans', 'Klok', '6666AA', 12, 'HansKlok@gmail.be', 06000000001, GETDATE())
	

) AS Source (Gebruikersnaam, Wachtwoord, Rol, Voornaam, Tussenvoegsel, Achternaam, Postcode, Huisnummer, Email, Telefoonnummer, Datum_certificering, Einddatum_certificering, IBAN,Laatste_weiziging)
On 
	Target.Gebruikersnaam = Source.Gebruikersnaam
WHEN NOT MATCHED BY TARGET THEN	
INSERT (Gebruikersnaam, Wachtwoord, Rol, Voornaam, Tussenvoegsel, Achternaam, Postcode, Huisnummer, Email, Telefoonnummer, Datum_certificering, Einddatum_certificering, IBAN,Laatste_weiziging);
