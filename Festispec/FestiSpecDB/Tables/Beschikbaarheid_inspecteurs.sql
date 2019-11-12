CREATE TABLE [dbo].[Beschikbaarheid_inspecteurs]
(
	[MedewerkerID] INT NOT NULL, 
	[Datum] DATE NOT NULL, 
    FOREIGN KEY(MedewerkerID) REFERENCES Account(AccountID) ON UPDATE CASCADE ON DELETE CASCADE,
	PRIMARY KEY([MedewerkerID], [Datum])
)
