CREATE VIEW [dbo].[viewProductDetails]
	AS 
	SELECT
	Y.Timestamp As DateAdded
	,Y.Id As StockId
	 ,Products.Id As ProductId
	 ,X.Id As StockItemId
	 ,Products.ProductName
	 ,Products.GenericName
	 ,Products.Origin
	 ,X.Strength
	 ,X.Quantity
	 ,X.PackSize
	 ,X.QtyPackSize
	 ,X.ReorderLevel
	 ,X.BatchNo
	 ,X.ExpiryDate
	 ,X.Location
	 ,X.MajorSupplier
	 ,X.CostPerUnit
	 ,X.TotalCost
	 ,X.WarehouseNo
	FROM Products with (nolock)
	INNER JOIN
	(
	SELECT * 
	FROM StockItems with (nolock)
	) As X
	ON 
	X.ProductId = Products.Id
	LEFT JOIN
	(
	SELECT * 
	FROM Stock with (nolock)
	) As Y
	ON 
	Y.Id = X.StockId


