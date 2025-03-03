﻿namespace DeployManager.Test.Entities
{
    public enum DeployType
    {
        Production         = 1,
        ProductionStaging  = 2,
        Development        = 3,
        DevelopmentStaging = 4,
    }

    public enum ServerType
    {
        AccountApi         = 1,
        DeployServer       = 2,
        FileServer         = 3,
        FileServerWorker   = 4,
        LogServer          = 5,
        LoginServer        = 6,
        MailWorker         = 7,
        RegServer          = 8,
        RmsApiServer       = 9,
        RmsWorker          = 10,
        SendApi            = 11,
        ShareServer        = 12,
        StorageServer      = 13,
        SubscribeApiServer = 14,
        SubscribeWorker    = 15,
        SupportPortal      = 16,
        WebApi             = 17,
    }
}
