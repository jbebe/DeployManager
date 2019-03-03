namespace DeployManagerService.Server

module ServerOperations =
    open DeployManagerService.Server.Entities

    let GetServerList: ServerListItem[] = [| 
         1, "StorageServer";
         2, "ShareServer";
         3, "LoginServer";
         4, "WebApi";
         5, "AccountApi";
         6, "SubscribeApi";
         7, "SendServer";
         8, "FileWorker";
         9, "MailWorker";
        10, "SubscribeWorker";
    |]