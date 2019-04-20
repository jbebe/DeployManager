CREATE PROCEDURE [dbo].[GenerateRandomId]
AS
BEGIN
    RETURN [dbo].[GenerateHash](NEWID())
END
