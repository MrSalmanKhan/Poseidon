CREATE TABLE [dbo].[Supplier]
(
	[Id] INT NOT NULL Identity PRIMARY KEY, 
    [SupplierName] VARCHAR(128) NOT NULL, 
    [Address] VARCHAR(MAX) NULL, 
    [Phone] VARCHAR(16) NULL, 
    [Email] VARCHAR(64) NULL, 
    [Country] VARCHAR(32) NULL
)
