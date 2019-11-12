CREATE TABLE [dbo].[Ingeplande_inspecteurs]
(
	[OpdrachtID] INT NOT NULL, 
    [InspecteurID] INT NOT NULL,
	PRIMARY KEY([OpdrachtID], [InspecteurID]),
	FOREIGN KEY(OpdrachtID) REFERENCES Opdracht(OpdrachtID) ON UPDATE CASCADE ON DELETE CASCADE,
	FOREIGN KEY(InspecteurID) REFERENCES Account(AccountID) ON UPDATE CASCADE ON DELETE CASCADE
)
