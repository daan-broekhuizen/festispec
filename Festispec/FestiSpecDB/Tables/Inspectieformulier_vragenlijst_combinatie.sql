CREATE TABLE [dbo].[Inspectieformulier_vragenlijst_combinatie]
(
	[VIC_ID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[InspectieformulierID] INT NOT NULL, 
    [VraagID] INT NOT NULL,
	FOREIGN KEY(InspectieformulierID) REFERENCES Inspectieformulier(InspectieformulierID) ON DELETE CASCADE ON UPDATE CASCADE,
	FOREIGN KEY(VraagID) REFERENCES Vraag(VraagID) ON DELETE CASCADE ON UPDATE CASCADE
	
	
)
