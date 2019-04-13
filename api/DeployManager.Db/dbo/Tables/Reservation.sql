CREATE TABLE [dbo].[Reservation] (
    [Id]         VARCHAR (32)  NOT NULL,
    [DeployType] INT           NOT NULL,
    [ServerType] INT           NOT NULL,
    [BranchName] VARCHAR (512) NOT NULL,
    [Author]     VARCHAR (256) NOT NULL,
    [Start]      DATETIME      NOT NULL,
    [End]        DATETIME      NOT NULL,
    [Previous]   VARCHAR (32)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

