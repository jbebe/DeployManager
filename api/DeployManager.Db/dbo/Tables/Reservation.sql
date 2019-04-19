CREATE TABLE [dbo].[Reservation] (
    [Id]         VARCHAR(32)   NOT NULL,
    [DeployType] INT           NOT NULL,
    [ServerType] INT           NOT NULL,
    [BranchName] VARCHAR(256)  NOT NULL,
    [UserId]     VARCHAR(32)   NOT NULL,
    [Start]      DATETIME      NOT NULL,
    [End]        DATETIME      NOT NULL,

    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT FK_Reservation_UserId     FOREIGN KEY ([UserId])     REFERENCES [dbo].[User]       ([Id]),
    CONSTRAINT FK_Reservation_DeployType FOREIGN KEY ([DeployType]) REFERENCES [dbo].[DeployType] ([Id]),
    CONSTRAINT FK_Reservation_ServerType FOREIGN KEY ([ServerType]) REFERENCES [dbo].[ServerType] ([Id])
);
GO

GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[Reservation] TO [deploymanager]
    AS [dbo];
GO

