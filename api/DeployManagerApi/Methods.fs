namespace DeployManagerApi

module Methods =
    
    open DeployManagerService.Server

    let GetServerList: ServerListResponse = { 
        Data = ServerOperations.GetServerList 
            |> Array.map (fun (id, name) -> { Id = id; Name = name })
    }

