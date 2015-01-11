/****** Object:  Table [dbo].[security_EntitiesGroups]    Script Date: 11.01.2015 15:21:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[security_EntitiesGroups](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[security_EntityReferences]    Script Date: 11.01.2015 15:21:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[security_EntityReferences](
	[Id] [uniqueidentifier] NOT NULL,
	[EntitySecurityKey] [uniqueidentifier] NOT NULL,
	[Type] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[EntitySecurityKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[security_EntityReferencesToEntitiesGroups]    Script Date: 11.01.2015 15:21:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[security_EntityReferencesToEntitiesGroups](
	[GroupId] [uniqueidentifier] NOT NULL,
	[EntityReferenceId] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[GroupId] ASC,
	[EntityReferenceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[security_EntityTypes]    Script Date: 11.01.2015 15:21:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[security_EntityTypes](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[security_Operations]    Script Date: 11.01.2015 15:21:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[security_Operations](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Comment] [nvarchar](255) NULL,
	[Parent] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[security_Permissions]    Script Date: 11.01.2015 15:21:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[security_Permissions](
	[Id] [uniqueidentifier] NOT NULL,
	[EntitySecurityKey] [uniqueidentifier] NULL,
	[Allow] [bit] NOT NULL,
	[Level] [int] NOT NULL,
	[EntityTypeName] [nvarchar](255) NULL,
	[Operation] [uniqueidentifier] NOT NULL,
	[User] [bigint] NULL,
	[UsersGroup] [uniqueidentifier] NULL,
	[EntitiesGroup] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[security_UsersGroups]    Script Date: 11.01.2015 15:21:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[security_UsersGroups](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Parent] [uniqueidentifier] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[security_UsersGroupsHierarchy]    Script Date: 11.01.2015 15:21:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[security_UsersGroupsHierarchy](
	[ParentGroup] [uniqueidentifier] NOT NULL,
	[ChildGroup] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ChildGroup] ASC,
	[ParentGroup] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[security_UsersToUsersGroups]    Script Date: 11.01.2015 15:21:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[security_UsersToUsersGroups](
	[GroupId] [uniqueidentifier] NOT NULL,
	[UserId] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[GroupId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 11.01.2015 15:21:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[security_EntityReferences]  WITH CHECK ADD  CONSTRAINT [FKBBE4029387CC6C80] FOREIGN KEY([Type])
REFERENCES [dbo].[security_EntityTypes] ([Id])
GO
ALTER TABLE [dbo].[security_EntityReferences] CHECK CONSTRAINT [FKBBE4029387CC6C80]
GO
ALTER TABLE [dbo].[security_EntityReferencesToEntitiesGroups]  WITH CHECK ADD  CONSTRAINT [FK17A323D6DDB11ADF] FOREIGN KEY([EntityReferenceId])
REFERENCES [dbo].[security_EntityReferences] ([Id])
GO
ALTER TABLE [dbo].[security_EntityReferencesToEntitiesGroups] CHECK CONSTRAINT [FK17A323D6DDB11ADF]
GO
ALTER TABLE [dbo].[security_EntityReferencesToEntitiesGroups]  WITH CHECK ADD  CONSTRAINT [FK17A323D6DE03A26A] FOREIGN KEY([GroupId])
REFERENCES [dbo].[security_EntitiesGroups] ([Id])
GO
ALTER TABLE [dbo].[security_EntityReferencesToEntitiesGroups] CHECK CONSTRAINT [FK17A323D6DE03A26A]
GO
ALTER TABLE [dbo].[security_Operations]  WITH CHECK ADD  CONSTRAINT [FKE58BBFF82B7CDCD3] FOREIGN KEY([Parent])
REFERENCES [dbo].[security_Operations] ([Id])
GO
ALTER TABLE [dbo].[security_Operations] CHECK CONSTRAINT [FKE58BBFF82B7CDCD3]
GO
ALTER TABLE [dbo].[security_Permissions]  WITH CHECK ADD  CONSTRAINT [FKEA223C4C2EE8F612] FOREIGN KEY([UsersGroup])
REFERENCES [dbo].[security_UsersGroups] ([Id])
GO
ALTER TABLE [dbo].[security_Permissions] CHECK CONSTRAINT [FKEA223C4C2EE8F612]
GO
ALTER TABLE [dbo].[security_Permissions]  WITH CHECK ADD  CONSTRAINT [FKEA223C4C6C8EC3A5] FOREIGN KEY([EntitiesGroup])
REFERENCES [dbo].[security_EntitiesGroups] ([Id])
GO
ALTER TABLE [dbo].[security_Permissions] CHECK CONSTRAINT [FKEA223C4C6C8EC3A5]
GO
ALTER TABLE [dbo].[security_Permissions]  WITH CHECK ADD  CONSTRAINT [FKEA223C4C71C937C7] FOREIGN KEY([Operation])
REFERENCES [dbo].[security_Operations] ([Id])
GO
ALTER TABLE [dbo].[security_Permissions] CHECK CONSTRAINT [FKEA223C4C71C937C7]
GO
ALTER TABLE [dbo].[security_Permissions]  WITH CHECK ADD  CONSTRAINT [FKEA223C4CFC8C2B95] FOREIGN KEY([User])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[security_Permissions] CHECK CONSTRAINT [FKEA223C4CFC8C2B95]
GO
ALTER TABLE [dbo].[security_UsersGroups]  WITH CHECK ADD  CONSTRAINT [FKEC3AF233D0CB87D0] FOREIGN KEY([Parent])
REFERENCES [dbo].[security_UsersGroups] ([Id])
GO
ALTER TABLE [dbo].[security_UsersGroups] CHECK CONSTRAINT [FKEC3AF233D0CB87D0]
GO
ALTER TABLE [dbo].[security_UsersGroupsHierarchy]  WITH CHECK ADD  CONSTRAINT [FK69A3B61FA860AB70] FOREIGN KEY([ChildGroup])
REFERENCES [dbo].[security_UsersGroups] ([Id])
GO
ALTER TABLE [dbo].[security_UsersGroupsHierarchy] CHECK CONSTRAINT [FK69A3B61FA860AB70]
GO
ALTER TABLE [dbo].[security_UsersGroupsHierarchy]  WITH CHECK ADD  CONSTRAINT [FK69A3B61FA87BAE50] FOREIGN KEY([ParentGroup])
REFERENCES [dbo].[security_UsersGroups] ([Id])
GO
ALTER TABLE [dbo].[security_UsersGroupsHierarchy] CHECK CONSTRAINT [FK69A3B61FA87BAE50]
GO
ALTER TABLE [dbo].[security_UsersToUsersGroups]  WITH CHECK ADD  CONSTRAINT [FK7817F27A1238D4D4] FOREIGN KEY([GroupId])
REFERENCES [dbo].[security_UsersGroups] ([Id])
GO
ALTER TABLE [dbo].[security_UsersToUsersGroups] CHECK CONSTRAINT [FK7817F27A1238D4D4]
GO
ALTER TABLE [dbo].[security_UsersToUsersGroups]  WITH CHECK ADD  CONSTRAINT [FK7817F27AA6C99102] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
GO
ALTER TABLE [dbo].[security_UsersToUsersGroups] CHECK CONSTRAINT [FK7817F27AA6C99102]
GO
