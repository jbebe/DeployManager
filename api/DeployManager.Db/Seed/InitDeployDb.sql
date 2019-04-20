
-- User

declare @adminName varchar(MAX) = 'DESKTOP-EGHMKPJ\bebe'
declare @adminId varchar(32) = [dbo].[GenerateHash](@adminName)
insert into [dbo].[User] values (@adminId, @adminName, 1)

declare @userName varchar(MAX) = 'kovacs.janos'
declare @userId varchar(32) = [dbo].[GenerateHash](@userName)
insert into [dbo].[User] values (@userId, @userName, 1)

-- DeployType

insert into [dbo].[DeployType] values 
    (1, 'Production', 'Live slot. Everything here is reachable for the users.', 0),
    (2, 'ProdStaging', 'Staging slot for live. Everything here is either for final testing or waiting for swap.', 0),
    (3, 'Development', 'Development slot. Everything here is being tested before releasing to production.', 1),
    (4, 'DevelStaging', 'Staging slot for development. Only half-done new features appear here.', 1)

-- ServerType

insert into [dbo].[ServerType] values
    ( 1, 'AccountApi',         'The server''s name is AccountApi 1'),
    ( 2, 'DeployServer',       'The server''s name is DeployServer 2'),
    ( 3, 'FileServer',         'The server''s name is FileServer 3'),
    ( 4, 'FileServerWorker',   'The server''s name is FileServerWorker 4'),
    ( 5, 'LogServer',          'The server''s name is LogServer 5'),
    ( 6, 'LoginServer',        'The server''s name is LoginServer 6'),
    ( 7, 'MailWorker',         'The server''s name is MailWorker 7'),
    ( 8, 'RegServer',          'The server''s name is RegServer 8'),
    ( 9, 'RmsApiServer',       'The server''s name is RmsApiServer 9'),
    (10, 'RmsWorker',          'The server''s name is RmsWorker 10'),
    (11, 'SendApi',            'The server''s name is SendApi 11'),
    (12, 'ShareServer',        'The server''s name is ShareServer 12'),
    (13, 'StorageServer',      'The server''s name is StorageServer 13'),
    (14, 'SubscribeApiServer', 'The server''s name is SubscribeApiServer 14'),
    (15, 'SubscribeWorker',    'The server''s name is SubscribeWorker 15'),
    (16, 'SupportPortal',      'The server''s name is SupportPortal 16'),
    (17, 'WebApi',             'The server''s name is WebApi 17')

-- DeployPermission

insert into [dbo].[DeployPermission] values
    (@adminId, 4, 4),
    (@userId,  4, 3)

-- ServerInstance

insert into [dbo].[ServerInstance] values
    (4,  1, 1),
    (4,  2, 1),
    (4,  3, 1),
    (4,  4, 1),
    (4,  5, 1),
    (4,  6, 1),
    (4,  7, 1),
    (4,  8, 1),
    (4,  9, 1),
    (4, 10, 1),
    (4, 11, 1),
    (4, 12, 1),
    (4, 13, 1),
    (4, 14, 1),
    (4, 15, 1),
    (4, 16, 1),
    (4, 17, 1)

-- Reservation

declare @id varchar(32)

exec @id = [dbo].[GenerateRandomId]
insert into [dbo].[Reservation] values
    (@id, 4, 1, 'feature/AccountApi/FullRewrite', @adminId, SYSDATETIME(), DATEADD(W, 1, SYSDATETIME()))

exec @id = [dbo].[GenerateRandomId]
insert into [dbo].[Reservation] values
    (@id, 4, 2, 'feature/AccountApi/FullRewrite', @adminId, SYSDATETIME(), DATEADD(W, 1, SYSDATETIME()))

exec @id = [dbo].[GenerateRandomId]
insert into [dbo].[Reservation] values
    (@id, 4, 3, 'feature/AccountApi/FullRewrite', @adminId, SYSDATETIME(), DATEADD(W, 1, SYSDATETIME()))


exec @id = [dbo].[GenerateRandomId]
insert into [dbo].[Reservation] values
    (@id, 4, 4, 'feature/ShareServer/RefactorAll', @adminId, SYSDATETIME(), DATEADD(D, 2, SYSDATETIME()))

exec @id = [dbo].[GenerateRandomId]
insert into [dbo].[Reservation] values
    (@id, 4, 5, 'feature/ShareServer/RefactorAll', @adminId, SYSDATETIME(), DATEADD(D, 2, SYSDATETIME()))

exec @id = [dbo].[GenerateRandomId]
insert into [dbo].[Reservation] values
    (@id, 4, 6, 'feature/ShareServer/RefactorAll', @adminId, SYSDATETIME(), DATEADD(D, 2, SYSDATETIME()))


exec @id = [dbo].[GenerateRandomId]
insert into [dbo].[Reservation] values
    (@id, 4, 7, 'fix/SendServer/DecreaseReadability', @userId, SYSDATETIME(), DATEADD(D, 2, SYSDATETIME()))

exec @id = [dbo].[GenerateRandomId]
insert into [dbo].[Reservation] values
    (@id, 4, 8, 'fix/SendServer/DecreaseReadability', @userId, SYSDATETIME(), DATEADD(D, 2, SYSDATETIME()))

exec @id = [dbo].[GenerateRandomId]
insert into [dbo].[Reservation] values
    (@id, 4, 9, 'fix/SendServer/DecreaseReadability', @userId, SYSDATETIME(), DATEADD(D, 2, SYSDATETIME()))
