namespace DeployManager.Service.Entities
{
    public class ServerInfoEntity
    {
        public int Id => GenerateId(DeployType, ServerType);

        public DeployType DeployType { get; set; }

        public ServerType ServerType { get; set; }

        public string Url { get; set; }

        public static int GenerateId(DeployType deployType, ServerType serverType)
        {
            return ((int)serverType * 100) + (int)deployType;
        }
    }
}
