namespace DeployManagerDatabaseAccess

module Reservation =
    open System
    open DeployManagerDatabaseAccess.Redis
    open StackExchange.Redis

    let GetReservations from: DateTime =
        GetDatabase.SortedSetScan()
