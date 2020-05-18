USE [MunicipalityTax]
GO

/****** Object:  Table [dbo].[Tax]    Script Date: 2020-05-18 15:53:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Tax](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Municipality] [nvarchar](50) NOT NULL,
	[TaxAmount] [float] NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

