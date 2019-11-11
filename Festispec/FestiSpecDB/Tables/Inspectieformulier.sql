CREATE TABLE [dbo].[Inspectieformulier]
(
	[InspectieformulierID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [Datum_inspectie] DATE NULL, 
    [Locatie] TEXT NULL, 
    [OpdrachtID] INT NULL, 
    [Beschrijving] TEXT NULL, 
    [Laatste_weiziging] DATETIME NOT NULL,
	FOREIGN KEY(OpdrachtID) REFERENCES Opdracht(OpdrachtID)
)
