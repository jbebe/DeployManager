CREATE TABLE [dbo].[ServerInstance]
(
    [DeployType] INT NOT NULL,
    [ServerType] INT NOT NULL
    PRIMARY KEY CLUSTERED ([DeployType], [ServerType])
)
