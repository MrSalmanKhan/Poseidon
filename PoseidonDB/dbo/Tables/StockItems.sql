CREATE TABLE [dbo].[StockItems]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[ProductId] INT NULL,
	[StockId] INT NULL,
	[Strength] VARCHAR(64) NULL,
    [Quantity] INT NOT NULL DEFAULT 0, 
    [PackSize] VARCHAR(64) NULL, 
    [QtyPackSize] INT NULL, 
    [ReorderLevel] INT NULL, 
    [BatchNo] VARCHAR(64) NULL, 
    [ExpiryDate] DATETIME NULL, 
    [Location] VARCHAR(128) NULL, 
    [MajorSupplier] VARCHAR(64) NULL, 
    [CostPerUnit] DECIMAL(24, 2) NULL, 
    [TotalCost] DECIMAL(24, 2) NULL, 
    [WarehouseNo] INT NOT NULL DEFAULT 1,
	CONSTRAINT [FK_StockItems_Product] FOREIGN KEY ([ProductId]) REFERENCES [Products]([Id]),
	CONSTRAINT [FK_StockItems_Stock] FOREIGN KEY ([StockId]) REFERENCES [Stock]([Id])
)