open Suave
open Suave.Operators
open Suave.Filters
open Suave.Successful
open Suave.Writers
open DeployManagerApi
open Newtonsoft.Json
open DeployManagerApi.Config
open System.IO
open System

let IsNumericString (ns: string) =
    let isNumeric, _ = Int32.TryParse ns
    isNumeric

let Serialize obj =
    JsonConvert.SerializeObject(obj, JsonConfig.GetJsonConfigLazy.Value)

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
                GET >=> request (fun _ -> 
                    OK (Methods.GetServerList |> Serialize))
                >=> setMimeType JsonConfig.MimeType
            ]
            path "/api/v1/reservation" >=> choose [
                GET >=> request (fun _ -> 
                    OK (Methods.GetReservations |> Serialize))
                >=> setMimeType JsonConfig.MimeType
            ]
        ]
    Web.startWebServer cfg app

[<EntryPoint>]
let main argv = 
    match argv with
        | [| staticPath; portStr |] when Directory.Exists staticPath && IsNumericString portStr -> 
            RunWebServer staticPath (int portStr); 0
        | _ -> 
            printfn "Correct arguments: <static file path> <port>."; 1