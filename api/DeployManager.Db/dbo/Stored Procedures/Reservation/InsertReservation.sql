CREATE PROCEDURE [dbo].[InsertReservation]
    @deployType INT,
    @serverType INT,
    @branchName VARCHAR(512),
    @author VARCHAR(256),
    @start DATETIME,
    @end DATETIME,
    @previous VARCHAR(32) = NULL
AS
BEGIN
    IF @start >= @end
    BEGIN
        ;THROW 51000, 'Reservation start date cannot be after end', 1
    END

	DECLARE @id VARCHAR(32)
    SET @id = [dbo].[GenerateId](
        CONCAT(
            @deployType, 
            @serverType, 
            @branchName, 
            @author, 
            CONVERT(VARCHAR(23), @start, 126),
            CONVERT(VARCHAR(23), @end, 126)))

    IF @previous IS NOT NULL AND @previous NOT IN (SELECT Id FROM [dbo].[Reservation])
    BEGIN
        ;THROW 51000, 'Referenced reservation does not exist', 1
    END

    INSERT INTO [dbo].[Reservation]
        ([Id], [DeployType], [ServerType], [BranchName], [Author], [Start], [End], [Previous])
    VALUES
        (@id, @deployType, @serverType, @branchName, @author, @start, @end, @previous)
END