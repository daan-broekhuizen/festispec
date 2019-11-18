CREATE TABLE [dbo].[Antwoorden]
(
	[VIC_ID] INT NOT NULL, 
    [InspecteurID] INT NOT NULL, 
    [Antwoord_text] TEXT NULL,
	[Antwoord_image] IMAGE NULL,
    [Laatste_weiziging] DATETIME NOT NULL,
	FOREIGN KEY(VIC_ID) REFERENCES Inspectieformulier_vragenlijst_combinatie(VIC_ID) ON DELETE CASCADE ON UPDATE CASCADE,
	FOREIGN KEY(InspecteurID) REFERENCES Account(AccountID) ON DELETE CASCADE ON UPDATE CASCADE,
	PRIMARY KEY([VIC_ID], [InspecteurID])
)
