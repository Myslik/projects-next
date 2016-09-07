CREATE PROCEDURE [dbo].[SeedUsers]
AS
	SET IDENTITY_INSERT dbo.Users ON 

	MERGE INTO dbo.Users AS TARGET
	USING (VALUES (1, 'PremyslK', 'premyslkrajcovic@gmail.com')) 
		AS SOURCE (Id, UserName, Email)
	ON TARGET.Id = SOURCE.Id
	WHEN MATCHED THEN
		UPDATE SET 
			UserName = SOURCE.UserName,
			Email = SOURCE.Email
	WHEN NOT MATCHED BY TARGET THEN
		INSERT (Id, UserName, Email) VALUES (Id, UserName, Email)
	WHEN NOT MATCHED BY SOURCE THEN
		DELETE;

	SET IDENTITY_INSERT dbo.Users OFF
RETURN 0
