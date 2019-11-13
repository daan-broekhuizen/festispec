MERGE INTO dbo.Rol_lookup AS Target
USING (Values
	('in', 'Inspecteur'),
	('sm','Salesmedewerker'),
	('om', 'Operationeelmedewerker'),
	('ad', 'Admin'),
	('ma','Management')
	

) AS Source (Afkorting, Betekenis)
On 
	Target.Afkorting = Source.Afkorting
	
WHEN NOT MATCHED BY TARGET THEN
	INSERT (Afkorting, Betekenis)
	VALUES (Afkorting, Betekenis);
