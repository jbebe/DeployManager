CREATE PROCEDURE [dbo].[GetReservations]
    @id         VARCHAR (32) = NULL,
	@deployType INT          = NULL,
    @serverType INT          = NULL,
    @start      DATETIME     = NULL
AS
BEGIN
    IF @id IS NOT NULL
    BEGIN
        SELECT * 
        FROM [dbo].[Reservation] 
        WHERE [Id] = @id
    END
    ELSE
    BEGIN
        SELECT * 
        FROM [dbo].[Reservation]
        WHERE
            (@deployType IS NULL OR [DeployType] = @deployType) AND
            (@serverType IS NULL OR [ServerType] = @deployType) AND
            (@start      IS NULL OR [Start]      = @start)
    END
END