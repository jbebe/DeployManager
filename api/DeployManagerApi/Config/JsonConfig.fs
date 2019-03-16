namespace DeployManagerApi.Config

module JsonConfig =
    
    open Newtonsoft.Json
    open Newtonsoft.Json.Serialization

    let MimeType = "application/json; charset=utf-8"

    let GetJsonConfig = 
        let settings = new JsonSerializerSettings();
        settings.ContractResolver <- new CamelCasePropertyNamesContractResolver();
        settings
    let GetJsonConfigLazy = lazy (GetJsonConfig)