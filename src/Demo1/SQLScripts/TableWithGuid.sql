USE [Northwind]
GO

/****** Object:  Table [dbo].[TableWithGuid]    Script Date: 07.05.2016 14:13:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TableWithGuid](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](40) NOT NULL,
	
 CONSTRAINT [PK_TableWithGuid] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO