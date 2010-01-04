-- 'CuyaLightBox' Module Uninstall

-- Delete Module Tables

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_cm_LightboxImage_cm_LightBox]') AND parent_object_id = OBJECT_ID(N'[dbo].[cm_LightboxImage]'))
ALTER TABLE [dbo].[cm_LightboxImage] DROP CONSTRAINT [FK_cm_LightboxImage_cm_LightBox]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[cm_LightboxImage]') AND type in (N'U'))
DROP TABLE [dbo].[cm_LightboxImage]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_cm_LightBox_cuyahoga_node]') AND parent_object_id = OBJECT_ID(N'[dbo].[cm_LightBox]'))
ALTER TABLE [dbo].[cm_LightBox] DROP CONSTRAINT [FK_cm_LightBox_cuyahoga_node]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[cm_LightBox]') AND type in (N'U'))
DROP TABLE [dbo].[cm_LightBox]
GO

-- Delete System Database Entries

DELETE FROM cuyahoga_version WHERE assembly = 'Cuyahoga.Modules.CuyaLightBox'
GO

DELETE cuyahoga_modulesetting
FROM cuyahoga_modulesetting ms
INNER JOIN cuyahoga_moduletype mt ON mt.moduletypeid = ms.moduletypeid
AND mt.assemblyname = 'Cuyahoga.Modules.CuyaLightBox'
GO

DELETE cuyahoga_moduleservice
FROM cuyahoga_moduleservice ms
INNER JOIN cuyahoga_moduletype mt ON mt.moduletypeid = ms.moduletypeid AND mt.assemblyname = 'Cuyahoga.Modules.CuyaLightBox'
GO

DELETE FROM cuyahoga_moduletype
WHERE assemblyname = 'Cuyahoga.Modules.CuyaLightBox'
GO



--UNINSTALL STORED PROCEDURES

--IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROCEDURENAME]') AND type in (N'P'))
--BEGIN    
--	DROP PROCEDURE [dbo].[PROCEDURE_NAME]
--END
--GO



--UNINSTALL FUNCTIONS

--IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FUNCTIONNAME]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
--DROP FUNCTION [dbo].[FUNCTIONNAME]
--GO
