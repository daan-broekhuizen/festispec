CREATE TABLE [dbo].[Vraag]
(
	[VraagID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [Vraagstelling] TEXT NOT NULL, 
    [Vraagtype] NVARCHAR(2) NOT NULL, 
    [Bijlage] TEXT NULL, 
    [Laatste_weiziging] DATETIME NOT NULL,
	FOREIGN KEY(Vraagtype) REFERENCES Vraagtype_lookup(Afkorting)
)
