CREATE TABLE [dbo].[Klant]
(
	[KvK_nummer] INT NOT NULL PRIMARY KEY, 
    [Naam] VARCHAR(45) NOT NULL, 
    [Postcode] VARCHAR(6) NOT NULL, 
    [Huisnummer] VARCHAR(4) NOT NULL, 
    [Email] VARCHAR(100) NOT NULL, 
    [Website] VARCHAR(100) NULL, 
    [Laatste_weiziging] DATETIME NOT NULL
)
