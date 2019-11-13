MERGE INTO dbo.Vraagtype_lookup AS Target
USING (Values
	('ov', 'Open vraag'),
	('mv','Meerkeuze vraag'),
	('av', 'Afbeelding vraag'),
	('lv','Lijst vraag'),
	('sv', 'Schaal vraag')

) AS Source (Afkorting, Beschrijving)
On 
	Target.Afkorting = Source.Afkorting
	
WHEN NOT MATCHED BY TARGET THEN
	INSERT (Afkorting, Beschrijving)
	VALUES (Afkorting, Beschrijving);

