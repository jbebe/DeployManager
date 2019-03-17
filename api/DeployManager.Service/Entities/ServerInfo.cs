namespace DeployManager.Service.Entities
{
    public class ServerInfoEntity
    {
        public DeployType DeployType { get; set; }

        public ServerType ServerType { get; set; }

        public string Url { get; set; }
    }
}
