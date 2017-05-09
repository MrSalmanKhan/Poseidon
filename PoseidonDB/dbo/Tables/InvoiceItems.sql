

CREATE TABLE [dbo].[InvoiceItems](
	[Id] [int] NOT NULL,
	[InvoiceId] [int] NOT NULL,
	[StockItemId] [int] NOT NULL,
	[Qty] [int] NOT NULL,
	[SellingPrice] [decimal](24, 2) NOT NULL,
	[Tax] [decimal](14, 2) NOT NULL,
	[Discount] [decimal](14, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[InvoiceItems] ADD  DEFAULT ((0)) FOR [SellingPrice]
GO

ALTER TABLE [dbo].[InvoiceItems] ADD  DEFAULT ((0)) FOR [Tax]
GO

ALTER TABLE [dbo].[InvoiceItems] ADD  DEFAULT ((0)) FOR [Discount]
GO

ALTER TABLE [dbo].[InvoiceItems]  WITH CHECK ADD FOREIGN KEY([StockItemId])
REFERENCES [dbo].[StockItems] ([Id])



