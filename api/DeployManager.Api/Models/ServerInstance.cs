using System;
using System.Collections.Generic;

namespace DeployManager.Api.Models
{
    public partial class ServerInstance
    {
        public int DeployType { get; set; }
        public int ServerType { get; set; }
        public bool Available { get; set; }

        public DeployType DeployTypeNavigation { get; set; }
        public ServerType ServerTypeNavigation { get; set; }
    }
}
