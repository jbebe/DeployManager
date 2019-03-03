open Suave
open Suave.Operators
open Suave.Filters
open Suave.Successful
open DeployManagerApi
open Newtonsoft.Json
open Newtonsoft.Json.Serialization

let GetJsonConfig = 
    let settings = new JsonSerializerSettings();
    settings.ContractResolver <- new CamelCasePropertyNamesContractResolver();
    settings
let GetJsonConfigLazy = lazy (GetJsonConfig)

let RunWebServer staticPath portNumber = 
    let cfg = { 
        defaultConfig with
            homeFolder = Some(staticPath)
            bindings = [ 
                HttpBinding.createSimple HTTP "0.0.0.0" portNumber ] }
    let app = 
        choose [
            GET >=> choose [
                path "/" >=> OK "test" ; Files.browseHome
            ]
            path "/api/v1/server/list" >=> choose [
                GET >=> request (fun r -> 
                    OK (JsonConvert.SerializeObject(Methods.GetServerList, GetJsonConfigLazy.Value))
                )
                >=> Suave.Writers.setMimeType "application/json; charset=utf-8"
            ]
        ]
    Web.startWebServer cfg app

[<EntryPoint>]
let main argv = 
    match argv with
        | [| staticPath; portStr |] -> 
            RunWebServer staticPath (portStr |> int)
            0
        | _ -> 
            printfn "Correct arguments: <static file path> <port>."
            1