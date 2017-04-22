CREATE TABLE [dbo].[Products]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[DateAdded] DateTime NOT NULL,
    [ProductName] VARCHAR(128) NOT NULL, 
    [GenericName] VARCHAR(128) NULL,
    [Origin] VARCHAR(64) NULL
)
