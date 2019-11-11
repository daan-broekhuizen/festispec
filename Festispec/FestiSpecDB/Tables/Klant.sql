CREATE TABLE [dbo].[Klant]
(
	[KvK_nummer] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [Naam] NVARCHAR(45) NOT NULL, 
    [Postcode] NVARCHAR(6) NOT NULL, 
    [Huisnummer] INT NOT NULL, 
    [Email] NVARCHAR(130) NOT NULL, 
    [Website] NVARCHAR(100) NULL, 
    [Laatste_weiziging] DATETIME NOT NULL
)
