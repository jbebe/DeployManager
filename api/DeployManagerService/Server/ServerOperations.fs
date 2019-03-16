namespace DeployManagerService.Server

module ServerOperations =
    
    open System
    open DeployManagerService
    open DeployManagerService.Server.Entities

    let GetServerList: ServerListItem[] = 
        let values = Enum.GetValues(typeof<Enums.ServerType>) |> Seq.cast<int> |> Seq.toArray
        let names = Enum.GetNames(typeof<Enums.ServerType>)
        (values, names) ||> Array.zip