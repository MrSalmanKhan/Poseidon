CREATE TABLE [dbo].[SoldItems](
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[ProductName] [varchar](128) NOT NULL,
	[Strength] [varchar](64) NULL,
	[GenericName] [varchar](128) NULL,
	[Quantity] [int] NOT NULL,
	[PackSize] [varchar](64) NULL,
	[QtyPackSize] [int] NULL,
	[ReorderLevel] [int] NULL,
	[BatchNo] [varchar](64) NULL,
	[ExpiryDate] [datetime] NULL,
	[DateSold] [datetime] NULL,
	[Location] [varchar](128) NULL,
	[MajorSupplier] [varchar](64) NULL,
	[Origin] [varchar](64) NULL,
	[CostPerUnit] [decimal](14, 2) NULL,
	[TotalCost] [decimal](14, 2) NULL
	)