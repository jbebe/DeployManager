-- USE master

CREATE LOGIN deploymanager WITH PASSWORD = 'asdasd'
GO

-- USE DeployManager

CREATE USER deploymanager FOR LOGIN deploymanager
GO

-- GRANT ACCESS ON TABLES:
-- SELECT 'GRANT SELECT, UPDATE, INSERT, DELETE ON [' + TABLE_SCHEMA + '].[' + TABLE_NAME + '] TO [deploymanager]' FROM information_schema.tables