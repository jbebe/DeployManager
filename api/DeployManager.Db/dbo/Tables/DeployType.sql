CREATE TABLE [dbo].[DeployType]
(
    [Id]          INT          NOT NULL,
    [Name]        VARCHAR(128) NOT NULL,
    [Description] VARCHAR(512) NOT NULL,
    [Available]   BIT          NOT NULL,

    PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO

GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[DeployType] TO [deploymanager]
    AS [dbo];
GO
