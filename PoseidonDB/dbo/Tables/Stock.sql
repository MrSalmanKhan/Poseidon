CREATE TABLE [dbo].[Stock]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
    [SupplierId] INT NULL,
    [Timestamp] DATETIME NOT NULL,
    [GrandTotal] DECIMAL(14, 2) NULL, 
	CONSTRAINT [FK_Stock_Supplier] FOREIGN KEY ([SupplierId]) REFERENCES [Supplier]([Id])
)
