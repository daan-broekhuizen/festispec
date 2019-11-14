CREATE TABLE [dbo].[Vraag_mogelijk_antwoord]
(
	[VraagID] INT NOT NULL, 
    [Antwoord_nummer] INT NOT NULL,
	[Antwoord_text] TEXT NOT NULL,
	PRIMARY KEY([VraagID], [Antwoord_nummer]),
	FOREIGN KEY(VraagID) REFERENCES Vraag(VraagID)
)
