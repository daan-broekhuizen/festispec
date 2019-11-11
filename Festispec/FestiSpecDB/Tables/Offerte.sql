CREATE TABLE [dbo].[Offerte]
(
	[OfferteID] INT NOT NULL PRIMARY KEY, 
    [OpdrachtID] INT NOT NULL, 
    [Totaalbedrag] DECIMAL(10, 2) NULL, 
    [Aanmaakdatum] DATE NOT NULL, 
    [Beschrijving] TEXT NULL, 
    [Klantbeslissing_reden] TEXT NULL, 
    [Laatste_weiziging] DATETIME NOT NULL,
	FOREIGN KEY(OpdrachtID) REFERENCES Opdracht(OpdrachtID) ON UPDATE CASCADE ON DELETE CASCADE
)
