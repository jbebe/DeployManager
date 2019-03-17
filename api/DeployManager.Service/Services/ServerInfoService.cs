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
                new ServerInfoEntity { DeployType = DeployType.Production, Url = "accountapi.tresorit.com",      ServerType = ServerType.AccountApi },
                new ServerInfoEntity { DeployType = DeployType.Production, Url = "deploy.tresorit.com",          ServerType = ServerType.DeployServer },
                new ServerInfoEntity { DeployType = DeployType.Production, Url = "storage.tresorit.com",         ServerType = ServerType.FileServer },
                new ServerInfoEntity { DeployType = DeployType.Production, Url = null,                           ServerType = ServerType.FileServerWorker },
                new ServerInfoEntity { DeployType = DeployType.Production, Url = "log.tresorit.com",             ServerType = ServerType.LogServer },
                new ServerInfoEntity { DeployType = DeployType.Production, Url = "login.tresorit.com",           ServerType = ServerType.LoginServer },
                new ServerInfoEntity { DeployType = DeployType.Production, Url = null,                           ServerType = ServerType.MailWorker },
                new ServerInfoEntity { DeployType = DeployType.Production, Url = "register.tresorit.com",        ServerType = ServerType.RegServer },
                new ServerInfoEntity { DeployType = DeployType.Production, Url = "rmsapi.tresorit.com",          ServerType = ServerType.RmsApiServer },
                new ServerInfoEntity { DeployType = DeployType.Production, Url = null,                           ServerType = ServerType.RmsWorker },
                new ServerInfoEntity { DeployType = DeployType.Production, Url = "sendapi.tresorit.com",         ServerType = ServerType.SendApi },
                new ServerInfoEntity { DeployType = DeployType.Production, Url = "share.tresorit.com",           ServerType = ServerType.ShareServer },
                new ServerInfoEntity { DeployType = DeployType.Production, Url = "storage2.tresorit.com",        ServerType = ServerType.StorageServer },
                new ServerInfoEntity { DeployType = DeployType.Production, Url = "subscribeapi.tresorit.com",    ServerType = ServerType.SubscribeApiServer },
                new ServerInfoEntity { DeployType = DeployType.Production, Url = null,                           ServerType = ServerType.SubscribeWorker },
                new ServerInfoEntity { DeployType = DeployType.Production, Url = "internalsupport.tresorit.com", ServerType = ServerType.SupportPortal },
                new ServerInfoEntity { DeployType = DeployType.Production, Url = "webapi.tresorit.com",          ServerType = ServerType.WebApi },

                new ServerInfoEntity { DeployType = DeployType.ProductionStaging, Url = "accountapi.tresorit.com",      ServerType = ServerType.AccountApi },
                new ServerInfoEntity { DeployType = DeployType.ProductionStaging, Url = "deploy.tresorit.com",          ServerType = ServerType.DeployServer },
                new ServerInfoEntity { DeployType = DeployType.ProductionStaging, Url = "storage.tresorit.com",         ServerType = ServerType.FileServer },
                new ServerInfoEntity { DeployType = DeployType.ProductionStaging, Url = "log.tresorit.com",             ServerType = ServerType.LogServer },
                new ServerInfoEntity { DeployType = DeployType.ProductionStaging, Url = "login.tresorit.com",           ServerType = ServerType.LoginServer },
                new ServerInfoEntity { DeployType = DeployType.ProductionStaging, Url = "register.tresorit.com",        ServerType = ServerType.RegServer },
                new ServerInfoEntity { DeployType = DeployType.ProductionStaging, Url = "rmsapi.tresorit.com",          ServerType = ServerType.RmsApiServer },
                new ServerInfoEntity { DeployType = DeployType.ProductionStaging, Url = "sendapi.tresorit.com",         ServerType = ServerType.SendApi },
                new ServerInfoEntity { DeployType = DeployType.ProductionStaging, Url = "share.tresorit.com",           ServerType = ServerType.ShareServer },
                new ServerInfoEntity { DeployType = DeployType.ProductionStaging, Url = "storage2.tresorit.com",        ServerType = ServerType.StorageServer },
                new ServerInfoEntity { DeployType = DeployType.ProductionStaging, Url = "subscribeapi.tresorit.com",    ServerType = ServerType.SubscribeApiServer },
                new ServerInfoEntity { DeployType = DeployType.ProductionStaging, Url = "internalsupport.tresorit.com", ServerType = ServerType.SupportPortal },
                new ServerInfoEntity { DeployType = DeployType.ProductionStaging, Url = "webapi.tresorit.com",          ServerType = ServerType.WebApi },

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

                new ServerInfoEntity { DeployType = DeployType.DevelopmentStaging, Url = "accountapi.tresorit.com",      ServerType = ServerType.AccountApi },
                new ServerInfoEntity { DeployType = DeployType.DevelopmentStaging, Url = "deploy.tresorit.com",          ServerType = ServerType.DeployServer },
                new ServerInfoEntity { DeployType = DeployType.DevelopmentStaging, Url = "storage.tresorit.com",         ServerType = ServerType.FileServer },
                new ServerInfoEntity { DeployType = DeployType.DevelopmentStaging, Url = "log.tresorit.com",             ServerType = ServerType.LogServer },
                new ServerInfoEntity { DeployType = DeployType.DevelopmentStaging, Url = "login.tresorit.com",           ServerType = ServerType.LoginServer },
                new ServerInfoEntity { DeployType = DeployType.DevelopmentStaging, Url = "register.tresorit.com",        ServerType = ServerType.RegServer },
                new ServerInfoEntity { DeployType = DeployType.DevelopmentStaging, Url = "rmsapi.tresorit.com",          ServerType = ServerType.RmsApiServer },
                new ServerInfoEntity { DeployType = DeployType.DevelopmentStaging, Url = "sendapi.tresorit.com",         ServerType = ServerType.SendApi },
                new ServerInfoEntity { DeployType = DeployType.DevelopmentStaging, Url = "share.tresorit.com",           ServerType = ServerType.ShareServer },
                new ServerInfoEntity { DeployType = DeployType.DevelopmentStaging, Url = "storage2.tresorit.com",        ServerType = ServerType.StorageServer },
                new ServerInfoEntity { DeployType = DeployType.DevelopmentStaging, Url = "subscribeapi.tresorit.com",    ServerType = ServerType.SubscribeApiServer },
                new ServerInfoEntity { DeployType = DeployType.DevelopmentStaging, Url = "internalsupport.tresorit.com", ServerType = ServerType.SupportPortal },
                new ServerInfoEntity { DeployType = DeployType.DevelopmentStaging, Url = "webapi.tresorit.com",          ServerType = ServerType.WebApi },
            };
        }
    }
}
