using DeployManager.Service.Entities;
using System.Collections.Generic;

namespace DeployManager.Service.Services
{
    public class ServerInfoService
    {
        public IEnumerable<ServerInfoEntity> GetAllServerInfos()
        {
            return new[]
            {
                new ServerInfoEntity { DeployType = DeployType.Development, Url = "accountapi.tresorit.com",      ServerType = ServerType.AccountApi },
                new ServerInfoEntity { DeployType = DeployType.Development, Url = "deploy.tresorit.com",          ServerType = ServerType.DeployServer },
                new ServerInfoEntity { DeployType = DeployType.Development, Url = "storage.tresorit.com",         ServerType = ServerType.FileServer },
                new ServerInfoEntity { DeployType = DeployType.Development, Url = null,                           ServerType = ServerType.FileServerWorker },
                new ServerInfoEntity { DeployType = DeployType.Development, Url = "log.tresorit.com",             ServerType = ServerType.LogServer },
                new ServerInfoEntity { DeployType = DeployType.Development, Url = "login.tresorit.com",           ServerType = ServerType.LoginServer },
                new ServerInfoEntity { DeployType = DeployType.Development, Url = null,                           ServerType = ServerType.MailWorker },
                new ServerInfoEntity { DeployType = DeployType.Development, Url = "register.tresorit.com",        ServerType = ServerType.RegServer },
                new ServerInfoEntity { DeployType = DeployType.Development, Url = "rmsapi.tresorit.com",          ServerType = ServerType.RmsApiServer },
                new ServerInfoEntity { DeployType = DeployType.Development, Url = null,                           ServerType = ServerType.RmsWorker },
                new ServerInfoEntity { DeployType = DeployType.Development, Url = "sendapi.tresorit.com",         ServerType = ServerType.SendApi },
                new ServerInfoEntity { DeployType = DeployType.Development, Url = "share.tresorit.com",           ServerType = ServerType.ShareServer },
                new ServerInfoEntity { DeployType = DeployType.Development, Url = "storage2.tresorit.com",        ServerType = ServerType.StorageServer },
                new ServerInfoEntity { DeployType = DeployType.Development, Url = "subscribeapi.tresorit.com",    ServerType = ServerType.SubscribeApiServer },
                new ServerInfoEntity { DeployType = DeployType.Development, Url = null,                           ServerType = ServerType.SubscribeWorker },
                new ServerInfoEntity { DeployType = DeployType.Development, Url = "internalsupport.tresorit.com", ServerType = ServerType.SupportPortal },
                new ServerInfoEntity { DeployType = DeployType.Development, Url = "webapi.tresorit.com",          ServerType = ServerType.WebApi },
            };
        }
    }
}
