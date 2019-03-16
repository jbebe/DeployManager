namespace DeployManagerService

module Enums =

    type DeployType = 
        Production              = 1
        | ProductionStaging     = 2 
        | Development           = 3
        | DevelopmentStaging    = 4

    type ServerType =
        StorageServer       = 1
        | ShareServer       = 2
        | LoginServer       = 3
        | WebApi            = 4
        | AccountApi        = 5
        | SubscribeApi      = 6
        | SendServer        = 7
        | FileWorker        = 8
        | MailWorker        = 9
        | SubscribeWorker   = 10