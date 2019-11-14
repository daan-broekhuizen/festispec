MERGE INTO dbo.Status_lookup AS Target
USING (Values
	('no', 'Nieuwe opdracht'),
	('ov','Offerte verstuurt'),
	('og', 'Offerte geweigerd'),
	('oa', 'Offerte geaccepteerd'),
	('ia','Inspectieformulier aangemaakt'),
	('iu', 'Inspectie uitgevoerd'),
	('ro','Rapport opgesteld'),
	('rv','Rekening verstuurt'),
	('ob','Opdracht beindingt'),
	('pg','Opdracht geannuleerd')
	

) AS Source (Afkorting, Betekenis)
On 
	Target.Afkorting = Source.Afkorting
	
WHEN NOT MATCHED BY TARGET THEN
	INSERT (Afkorting, Betekenis)
	VALUES (Afkorting, Betekenis);

