-- 'CuyaLightBox' Module
SET DATEFORMAT ymd

CREATE TABLE [dbo].[cm_LightBox](
	[LightBoxId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](64) NULL,
	[Description] [varchar](2048) NULL,
	[Created] [datetime] NULL,
	[Active] [bit] NULL,
	[NodeId] [int] NULL,
 CONSTRAINT [PK_LightBox] PRIMARY KEY CLUSTERED 
(
	[LightBoxId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[cm_LightBox]  WITH CHECK ADD  CONSTRAINT [FK_cm_LightBox_cuyahoga_node] FOREIGN KEY([NodeId])
REFERENCES [dbo].[cuyahoga_node] ([nodeid])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[cm_LightBox] CHECK CONSTRAINT [FK_cm_LightBox_cuyahoga_node]
GO


CREATE TABLE [dbo].[cm_LightboxImage](
	[LightBoxImageId] [int] IDENTITY(1,1) NOT NULL,
	[LightBoxId] [int] NOT NULL,
	[Filename] [varchar](64) NULL,
	[Title] [varchar](64) NULL,
	[AltText] [varchar](255) NULL,
	[Width] [int] NULL,
	[Height] [int] NULL,
	[Created] [datetime] NULL,
	[Active] [bit] NULL,
 CONSTRAINT [PK_cm_LightboxImage] PRIMARY KEY CLUSTERED 
(
	[LightBoxImageId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[cm_LightboxImage]  WITH CHECK ADD  CONSTRAINT [FK_cm_LightboxImage_cm_LightBox] FOREIGN KEY([LightBoxId])
REFERENCES [dbo].[cm_LightBox] ([LightBoxId])
GO

ALTER TABLE [dbo].[cm_LightboxImage] CHECK CONSTRAINT [FK_cm_LightboxImage_cm_LightBox]
GO



-- Module database entries and settings
DECLARE @moduletypeid int

INSERT INTO cuyahoga_moduletype ([name],assemblyname,classname,path,editpath,inserttimestamp,updatetimestamp) VALUES ('CuyaLightBox','Cuyahoga.Modules.CuyaLightBox','Cuyahoga.Modules.CuyaLightBox.CuyaLightBoxModule','Modules/CuyaLightBox/CuyaLightBox.ascx', 'Modules/CuyaLightBox/Admin/AdminCuyaLightBox.aspx', '2008-11-29 19:59:59.998', '2008-11-29 19:59:59.998')
SELECT @moduletypeid = Scope_Identity()

INSERT INTO cuyahoga_modulesetting (moduletypeid, name, friendlyname, settingdatatype, iscustomtype, isrequired) VALUES (@moduletypeid, 'LIGHTBOX_NAME', 'Current LightBox Id (set to 0 (zero) as default)', 'System.Int32', 0, 1)

--NOTE: The 'servicekey' must be lower case (ToLower must be used in the code so any upper case entries do not work)
INSERT INTO cuyahoga_moduleservice (moduletypeid, servicekey, servicetype, classtype)
VALUES (@moduletypeid, 'cuyalightbox.cuyalightboxdao', 'Cuyahoga.Modules.CuyaLightBox.DataAccess.ICuyaLightBoxDao, Cuyahoga.Modules.CuyaLightBox', 'Cuyahoga.Modules.CuyaLightBox.DataAccess.CuyaLightBoxDao, Cuyahoga.Modules.CuyaLightBox')
GO

INSERT INTO cuyahoga_version (assembly, major, minor, patch) VALUES ('Cuyahoga.Modules.CuyaLightBox', 1, 5, 2)
GO
