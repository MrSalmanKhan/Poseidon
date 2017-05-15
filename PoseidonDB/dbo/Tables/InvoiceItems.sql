CREATE TABLE [dbo].[InvoiceItems](
	[Id] int NOT NULL IDENTITY PRIMARY KEY,
	[InvoiceId] int NOT NULL,
	[StockItemId] int NOT NULL,
	[Qty] int NOT NULL,
	[SellingPrice] decimal(24, 2) NOT NULL,
	[Tax] decimal(14, 2) NULL DEFAULT ((0)),
	[Discount] decimal(14, 2) NULL DEFAULT ((0)),
	CONSTRAINT [FK_InvoiceItems_Invoice] FOREIGN KEY ([InvoiceId]) REFERENCES [Invoice]([Id]),
	CONSTRAINT [FK_InvoiceItems_StockItems] FOREIGN KEY ([StockItemId]) REFERENCES [StockItems]([Id]),
)