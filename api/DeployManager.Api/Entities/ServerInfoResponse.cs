using DeployManager.Service.Entities;

namespace DeployManager.Api.Entities
{
    public class ServerInfoResponse
    {
        public int DeployType { get; set; }

        public int ServerType { get; set; }

        public string Url { get; set; }

        public static ServerInfoResponse Create(ServerInfoEntity serverInfo)
        {
            return new ServerInfoResponse
            {
                DeployType = (int)serverInfo.DeployType,
                ServerType = (int)serverInfo.ServerType,
                Url = serverInfo.Url,
            };
        }
    }
}
