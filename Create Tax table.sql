USE [MunicipalityTax]
GO

/****** Object:  Table [dbo].[Tax]    Script Date: 2020-05-16 00:42:16 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Tax](
	[Municipality] [nvarchar](50) NOT NULL,
	[Tax] [float] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NULL
) ON [PRIMARY]
GO

