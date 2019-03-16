using DeployManager.Service.Entities;

namespace DeployManager.Api.Entities
{
    public class ServerInfoResponse: ServerInfoEntity
    {
        public static ServerInfoResponse Create(ServerInfoEntity serverInfo)
        {
            return new ServerInfoResponse
            {
                DeployType = serverInfo.DeployType,
                ServerType = serverInfo.ServerType,
                Url = serverInfo.Url,
            };
        }
    }
}
