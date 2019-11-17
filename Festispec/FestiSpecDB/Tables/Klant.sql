CREATE TABLE [dbo].[Klant]
(
	[KvK_nummer] BIGINT NOT NULL PRIMARY KEY, 
    [Naam] NVARCHAR(45) NOT NULL, 
    [Postcode] NVARCHAR(6) NOT NULL, 
    [Huisnummer] NVARCHAR(4) NOT NULL,
	[Telefoonnummer] BIGINT NOT NULL,
    [Email] NVARCHAR(130) NOT NULL, 
    [Website] NVARCHAR(100) NULL,
	[Klant_logo] IMAGE NULL,
    [Laatste_weiziging] DATETIME NOT NULL
)
