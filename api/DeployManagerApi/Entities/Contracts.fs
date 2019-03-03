namespace DeployManagerApi

open Newtonsoft.Json

[<JsonObject>]
type ServerListResponseItem = { 
    
    [<JsonProperty(PropertyName = "id")>]
    Id: int

    [<JsonProperty>]
    Name: string

}

[<JsonObject>]
type ServerListResponse = { 
    
    [<JsonProperty>]
    Data: ServerListResponseItem[]

}