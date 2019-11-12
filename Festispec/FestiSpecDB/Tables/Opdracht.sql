CREATE TABLE [dbo].[Opdracht]
(
	[OpdrachtID] INT NOT NULL PRIMARY KEY, 
    [Opdracht_naam] NVARCHAR(45) NOT NULL, 
    [Status] NVARCHAR(2) NOT NULL, 
    [Creatie_datum] DATE NOT NULL, 
    [KlantID] INT NOT NULL, 
    [MedewerkerID] INT NOT NULL, 
	[Gebruikte_rechtsgebieden] TEXT NULL,
    [Rapportage] TEXT NULL, 
    [Rapportage_uses_template] INT NULL, 
    [Laatste_weiziging] DATETIME NOT NULL
	FOREIGN KEY(MedewerkerID) REFERENCES Account(AccountID) ON UPDATE NO ACTION ON DELETE NO ACTION,
	FOREIGN KEY(Status) REFERENCES Status_lookup(Afkorting) ON UPDATE NO ACTION ON DELETE NO ACTION,
	FOREIGN KEY(KlantID) REFERENCES Klant(KvK_nummer) ON UPDATE NO ACTION ON DELETE NO ACTION,
	FOREIGN KEY(Rapportage_uses_template) REFERENCES Rapport_template([TemplateID]) ON UPDATE NO ACTION ON DELETE NO ACTION
)
