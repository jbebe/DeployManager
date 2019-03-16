namespace DeployManagerDatabaseAccess

module Redis =
    open StackExchange.Redis
    
    let connection = lazy (
        ConnectionMultiplexer.Connect("localhost")
    )

    let GetConnection = connection.Value

    let GetDatabase = GetConnection.GetDatabase()