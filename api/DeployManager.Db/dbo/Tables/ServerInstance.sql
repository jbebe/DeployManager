CREATE TABLE [dbo].[ServerInstance]
(
    [DeployType] INT NOT NULL,
    [ServerType] INT NOT NULL,
    [Available]  BIT NOT NULL,

    PRIMARY KEY CLUSTERED ([DeployType], [ServerType]),
    CONSTRAINT FK_ServerInstance_DeployType FOREIGN KEY ([DeployType]) REFERENCES [dbo].[DeployType] ([Id]),
    CONSTRAINT FK_ServerInstance_ServerType FOREIGN KEY ([ServerType]) REFERENCES [dbo].[ServerType] ([Id])
)
GO

GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[ServerInstance] TO [deploymanager]
    AS [dbo];
GO