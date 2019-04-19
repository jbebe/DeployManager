CREATE TABLE [dbo].[User]
(
    [Id]      VARCHAR(32)   NOT NULL,
    [Name]    NVARCHAR(256) NOT NULL,
    [Enabled] BIT           NOT NULL,

    PRIMARY KEY CLUSTERED ([Id])
)
GO

GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[User] TO [deploymanager]
    AS [dbo];
GO