namespace DeployManagerApi

open Newtonsoft.Json
open System

[<JsonObject>]
type ServerListResponseItem = { 
    
    [<JsonProperty>]
    Id: int

    [<JsonProperty>]
    Name: string
}

[<JsonObject>]
type ServerListResponse = { 
    
    [<JsonProperty>]
    Data: ServerListResponseItem[] 
}

// - - - - - - - - - - - - - - - - - - - - - - - 

[<JsonObject>]
type ReservationListResponseItemMetadata = { 

    [<JsonProperty>]
    BranchName: string

    [<JsonProperty>]
    Creator: string
}

[<JsonObject>]
type ReservationListResponseItem = { 
    
    [<JsonProperty>]
    DeployTypeId: int

    [<JsonProperty>]
    ServerId: int

    [<JsonProperty>]
    From: DateTime

    [<JsonProperty>]
    To: DateTime

    [<JsonProperty>]
    Metadata: ReservationListResponseItemMetadata
}

[<JsonObject>]
type ReservationListResponse = { 
    
    [<JsonProperty>]
    Data: ReservationListResponseItem[]

}

// - - - - - - - - - - - - - - - - - - - - - - - 