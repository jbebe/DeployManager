namespace DeployManager.Api.ApiEntities
{
    public class ServerInstanceResponse
    {
        public int DeployType { get; set; }
        public int ServerType { get; set; }

        public static ServerInstanceResponse Create(Models.ServerInstance instance)
            => new ServerInstanceResponse
            {
                DeployType = instance.DeployType,
                ServerType = instance.ServerType,
            };
    }    
}
