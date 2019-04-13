CREATE FUNCTION [dbo].[GenerateId]( @seed VARCHAR(MAX) )
RETURNS VARCHAR (32)
AS
BEGIN
    RETURN 
        SUBSTRING(
            CONVERT(
                VARCHAR(34), 
                HASHBYTES('MD5', @seed), 1), 
            3, 32)
END