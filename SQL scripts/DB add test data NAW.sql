DELETE FROM Vraag_mogelijk_antwoord
DELETE FROM Offerte
DELETE FROM Contactpersoon
DELETE FROM Antwoorden
DELETE FROM Inspectieformulier_vragenlijst_combinatie
DELETE FROM Ingeplande_inspecteurs
DELETE FROM Vraag
DELETE FROM Inspectieformulier
DELETE FROM Beschikbaarheid_inspecteurs
DELETE FROM Opdracht
DELETE FROM Account
DELETE FROM Klant
DELETE FROM Rapport_template

INSERT INTO Account (Gebruikersnaam, Wachtwoord, Rol, Voornaam, Tussenvoegsel, Achternaam, Stad, Straatnaam, Huisnummer, Email, Telefoonnummer, Datum_certificering, Einddatum_certificering, IBAN, Laatste_weiziging)
VALUES('HansKlok', 'ab123', 'in', 'Hans', null, 'Klok', 'Amsterdam', 'Dom H. van der Laanstraat', '10', 'HansKlok@gmail.be', '0495555555', GETDATE(), GETDATE(), 'RABO001', GETDATE())

INSERT INTO Account (Gebruikersnaam, Wachtwoord, Rol, Voornaam, Tussenvoegsel, Achternaam, Stad, Straatnaam, Huisnummer, Laatste_weiziging)
VALUES('FransDeWanks', 'ab123', 'om', 'Frans', 'de', 'Wanks', 'Amsterdam', 'David Vosstraat', '29', GETDATE())

INSERT INTO Account (Gebruikersnaam, Wachtwoord, Rol, Voornaam, Tussenvoegsel, Achternaam, Stad, Straatnaam, Huisnummer, Laatste_weiziging)
VALUES('JohnWong', 'ab123', 'sm', 'John', null, 'Wong', 'Tilburg','Nimrodstraat', '19', GETDATE())

INSERT INTO Klant (KvK_nummer, Naam, Stad, Straatnaam, Huisnummer, Telefoonnummer, Email, Website, Laatste_weiziging)
VALUES('293871', 'Bospop','s-Hertogenbosch', 'Van Voornestraat', '30', '0495678123','bospop@hotmail.com', 'www.bospop.nl', GETDATE())

INSERT INTO Contactpersoon (KlantID, Voornaam, Tussenvoegsel, Achternaam, Email, Telefoon, Notities, Laatste_weiziging)
VALUES('293871' , 'Dave', null, 'Davidson', 'DaveDavidson@gmail.com', '0611111111', 'Klaagt veel.', GETDATE())

INSERT INTO Opdracht (Opdracht_naam, Status, Creatie_datum, KlantID, MedewerkerID, Gebruikte_rechtsgebieden, Laatste_weiziging, Klantwensen)
VALUES('Inspectie Bospop', 'rv', GETDATE(), '293871', (SELECT AccountID FROM Account WHERE Gebruikersnaam = 'FransDeWanks'), null, GETDATE(), 'Wensen') 

INSERT INTO Offerte (OpdrachtID, Totaalbedrag, Aanmaakdatum, Beschrijving, Klantbeslissing_reden, Laatste_weiziging)
VALUES((SELECT OpdrachtID FROM Opdracht WHERE Opdracht_naam = 'Inspectie Bospop'), 2000.50, GETDATE(), 'we gaan een inspectie doen', 'ze vonden het goed', GETDATE())

INSERT INTO Inspectieformulier (InspectieFormulierTitel, Datum_inspectie, Locatie, OpdrachtID, Beschrijving, Laatste_weiziging)
VALUES('Inspectie Bospop festival', GETDATE(), 'Bospop festival terrein', (SELECT OpdrachtID FROM Opdracht WHERE Opdracht_naam = 'Inspectie Bospop'), 'Vul dit formulier in voor Bospop inspectie', GETDATE())

INSERT INTO Vraag(Vraagstelling, Vraagtype, Laatste_weiziging)
VALUES('Open vraag', 'ov', GETDATE())

INSERT INTO Vraag(Vraagstelling, Vraagtype, Laatste_weiziging)
VALUES('Meerkeuze vraag', 'mv', GETDATE())

INSERT INTO Vraag_mogelijk_antwoord(VraagID, Antwoord_nummer, Antwoord_text)
VALUES((SELECT VraagID FROM Vraag WHERE CONVERT(NVARCHAR,Vraagstelling) = 'Meerkeuze vraag'), 1, 'a')

INSERT INTO Vraag_mogelijk_antwoord(VraagID, Antwoord_nummer, Antwoord_text)
VALUES((SELECT VraagID FROM Vraag WHERE CONVERT(NVARCHAR, Vraagstelling) = 'Meerkeuze vraag'), 2, 'b')

INSERT INTO Vraag_mogelijk_antwoord(VraagID, Antwoord_nummer, Antwoord_text)
VALUES((SELECT VraagID FROM Vraag WHERE CONVERT(NVARCHAR, Vraagstelling) = 'Meerkeuze vraag'), 3, 'c')

INSERT INTO Beschikbaarheid_inspecteurs(MedewerkerID, Datum)
VALUES((SELECT AccountID FROM Account WHERE Gebruikersnaam = 'HansKlok'), GETDATE())

INSERT INTO Beschikbaarheid_inspecteurs(MedewerkerID, Datum)
VALUES((SELECT AccountID FROM Account WHERE Gebruikersnaam = 'HansKlok'), GETDATE() + 1)

INSERT INTO Beschikbaarheid_inspecteurs(MedewerkerID, Datum)
VALUES((SELECT AccountID FROM Account WHERE Gebruikersnaam = 'HansKlok'), GETDATE() + 2)

INSERT INTO Beschikbaarheid_inspecteurs(MedewerkerID, Datum)
VALUES((SELECT AccountID FROM Account WHERE Gebruikersnaam = 'HansKlok'), GETDATE() + 3)

INSERT INTO Ingeplande_inspecteurs(InspecteurID, OpdrachtID)
VALUES((SELECT AccountID FROM Account WHERE Gebruikersnaam = 'HansKlok'),(SELECT OpdrachtID FROM Opdracht WHERE Opdracht_naam = 'Inspectie Bospop'))
;