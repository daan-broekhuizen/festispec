﻿CREATE TABLE [dbo].[Account]
(
	[AccountID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [Gebruikersnaam] NVARCHAR(45) NOT NULL, 
    [Wachtwoord] NVARCHAR(45) NOT NULL, 
    [Rol] NVARCHAR(2) NOT NULL, 
    [Voornaam] NVARCHAR(30) NOT NULL, 
    [Tussenvoegsel] NVARCHAR(15) NULL, 
    [Achternaam] NVARCHAR(30) NOT NULL, 
    [Postcode] NVARCHAR(6) NULL, 
    [Huisnummer] NVARCHAR(4) NULL, 
    [Email] NVARCHAR(120) NULL, 
    [Telefoonnummer] INT NULL, 
    [Datum_certificering] DATE NULL, 
    [Einddatum_certificering] DATE NULL, 
    [IBAN] NVARCHAR(18) NULL, 
    [Laatste_weiziging] DATETIME NOT NULL,
	FOREIGN KEY(Rol) REFERENCES Rol_lookup(Afkorting)
)
