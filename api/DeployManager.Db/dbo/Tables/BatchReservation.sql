CREATE TABLE [dbo].[BatchReservation]
(
    [BatchId]       VARCHAR(32) NOT NULL,
    [ReservationId] VARCHAR(32) NOT NULL,

    PRIMARY KEY CLUSTERED ([BatchId], [ReservationId]),
    CONSTRAINT FK_BatchReservation_ReservationId FOREIGN KEY ([ReservationId]) REFERENCES [dbo].[Reservation] ([Id])
)
GO

GRANT SELECT, INSERT, UPDATE, DELETE
    ON OBJECT::[dbo].[BatchReservation] TO [deploymanager]
    AS [dbo];
GO