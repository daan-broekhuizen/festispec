CREATE TABLE [dbo].[Contactpersoon]
(
	[ContactpersoonID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [KlantID] BIGINT NOT NULL, 
    [Voornaam] NVARCHAR(30) NOT NULL, 
    [Tussenvoegsel] NVARCHAR(15) NULL, 
    [Achternaam] NVARCHAR(30) NOT NULL, 
    [Email] NVARCHAR(130) NOT NULL, 
    [Telefoon] BIGINT NOT NULL, 
    [Notities] TEXT NULL, 
    [Laatste_weiziging] DATETIME NOT NULL,
	FOREIGN KEY(KlantID) REFERENCES Klant(KvK_nummer) ON UPDATE CASCADE ON DELETE CASCADE
)
