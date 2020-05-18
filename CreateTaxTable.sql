USE [MunicipalityTax]
GO

/****** Object:  Table [dbo].[Tax]    Script Date: 2020-05-18 12:08:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Tax](
	[Municipality] [nvarchar](50) NOT NULL,
	[TaxAmount] [float] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NULL,
	[Type] [nvarchar](50) NOT NULL
) ON [PRIMARY]
GO


