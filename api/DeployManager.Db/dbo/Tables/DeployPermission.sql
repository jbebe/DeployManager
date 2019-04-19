CREATE TABLE [dbo].[DeployPermission]
(
    [UserId] VARCHAR(32) NOT NULL,
    [DeployType] INT NOT NULL,
    [Permission] INT NOT NULL,

    PRIMARY KEY CLUSTERED ([UserId], [DeployType]),
    CONSTRAINT FK_DeployPermission_DeployType FOREIGN KEY ([DeployType]) REFERENCES [dbo].[DeployType] ([Id])
)
GO

GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[DeployPermission] TO [deploymanager]
    AS [dbo];
GO