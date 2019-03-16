namespace DeployManagerService.Reservation

module Entities =
    
    open System
    open DeployManagerService.Enums

    type ReservationListItemMetadata = { 

        BranchName: string
        Creator: string
    }

    type ReservationListItem = {
        
        DeployType: DeployType
        Server: ServerType
        From: DateTime
        To: DateTime
        Metadata: ReservationListItemMetadata
    }
