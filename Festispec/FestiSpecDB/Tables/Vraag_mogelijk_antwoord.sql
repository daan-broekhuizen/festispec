CREATE TABLE [dbo].[Vraag_mogelijk_antwoord]
(
	[VraagID] INT IDENTITY(1,1) NOT NULL, 
    [Antwoord_nummer] INT NOT NULL,
	PRIMARY KEY([VraagID], [Antwoord_nummer]),
	FOREIGN KEY(VraagID) REFERENCES Vraag(VraagID)
)
