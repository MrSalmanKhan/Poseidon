CREATE TABLE [dbo].[Stock]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Timestamp] DATETIME NOT NULL, 
    [GrandTotal] DECIMAL(14, 2) NULL
)
