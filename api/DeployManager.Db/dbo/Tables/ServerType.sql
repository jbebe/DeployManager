CREATE TABLE [dbo].[ServerType]
(
    [Id]          INT           NOT NULL,
    [Name]        VARCHAR(128)  NOT NULL,
    [Description] NVARCHAR(512) NOT NULL,

    PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO

GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[ServerType] TO [deploymanager]
    AS [dbo];
GO