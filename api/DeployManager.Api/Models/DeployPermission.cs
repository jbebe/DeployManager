using System;
using System.Collections.Generic;

namespace DeployManager.Api.Models
{
    public partial class DeployPermission
    {
        public string UserId { get; set; }
        public int DeployType { get; set; }
        public int Permission { get; set; }

        public DeployType DeployTypeNavigation { get; set; }
    }
}
