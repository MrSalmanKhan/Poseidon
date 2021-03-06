﻿CREATE TABLE [dbo].[MonthlyPurchasesValues](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [varchar](128) NULL,
	[GenericName] [varchar](128) NULL,
	[Quantity] [int] NULL,
	[Date] [date] NULL,
	[WareHouseNo] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]