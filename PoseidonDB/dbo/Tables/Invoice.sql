CREATE TABLE [dbo].[Invoice](
	[Id] INT NOT NULL IDENTITY PRIMARY KEY,
	[TimeStamp] datetime NOT NULL,
	[CustomerId] int NULL,
	[TotalAmount] decimal(24, 2) NULL,
	[TotalTax] decimal(14, 2) NULL,
	[TotalDiscount] decimal(14, 3) NULL,
	CONSTRAINT [FK_Invoice_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [Customer]([Id])
	)
