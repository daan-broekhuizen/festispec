CREATE TABLE [dbo].[Contactpersoon]
(
	[ContactpersoonID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [KlantID] INT NOT NULL, 
    [Voornaam] NVARCHAR(30) NOT NULL, 
    [Tussenvoegsel] NVARCHAR(15) NOT NULL, 
    [Achternaam] NVARCHAR(30) NOT NULL, 
    [Email] NVARCHAR(130) NOT NULL, 
    [Telefoon] INT NOT NULL, 
    [Notities] TEXT NULL, 
    [Laatste_weiziging] DATETIME NOT NULL,
	FOREIGN KEY(KlantID) REFERENCES Klant(KvK_nummer) ON UPDATE CASCADE ON DELETE CASCADE
)
