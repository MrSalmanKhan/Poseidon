CREATE TABLE [dbo].[Medicine]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Alt] VARCHAR(8) NULL, 
    [Description] VARCHAR(128) NOT NULL, 
    [Pack] INT NULL, 
    [Quantity] INT NULL, 
    [BuyingPrice] DECIMAL(9, 2) NULL, 
    [Value] DECIMAL(9, 2) NULL, 
    [ExpiryDate] DATE NULL, 
    [Location] VARCHAR(128) NULL, 
    [IssueTo] VARCHAR(64) NULL, 
    [BatchNo] VARCHAR(32) NULL, 
    [UOM] VARCHAR(64) NULL, 
    [Origin] VARCHAR(64) NULL, 
    [SupplierId] INT NULL
	CONSTRAINT [FK_Medicine_Supplier] FOREIGN KEY ([SupplierId]) REFERENCES [dbo].[Supplier] ([Id]),
)
