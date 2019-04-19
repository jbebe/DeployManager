CREATE FUNCTION [dbo].[GenerateRandomId]()
RETURNS VARCHAR(32)
AS
BEGIN
    RETURN [dbo].[GenerateHash](FORMAT(RAND(), 'N17')) -- 17 is more than RAND() precision
END
