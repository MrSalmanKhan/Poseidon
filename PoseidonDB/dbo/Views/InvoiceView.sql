CREATE VIEW [dbo].[InvoiceView]
	AS SELECT 
	Invoice.Id As invoiceId,
	Invoice.TimeStamp,
	Invoice.CustomerId,
	Invoice.TotalAmount,
	Invoice.TotalDiscount,
	Invoice.TotalTax,
	InvoiceItems.Id As invoiceItemId,
	InvoiceItems.StockItemId As StockItemId,
	InvoiceItems.Qty,
	InvoiceItems.SellingPrice,
	InvoiceItems.Tax,
	InvoiceItems.Discount,
	Products.Id,
	Products.ProductName,
	Products.GenericName,
	Products.Origin

	 FROM [Invoice] inner join InvoiceItems
	 ON InvoiceItems.InvoiceId = Invoice.Id
	 Inner join StockItems 
	 ON  StockItems.StockId = InvoiceItems.StockItemId
	 Inner join Products 
	 ON StockItems.ProductId = Products.Id
