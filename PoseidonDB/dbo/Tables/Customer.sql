CREATE TABLE [dbo].[Customer]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CustomerName] VARCHAR(64) NULL, 
    [Age] INT NULL, 
    [Sex] VARCHAR(8) NULL, 
    [Address] VARBINARY(128) NULL, 
    [Phone] VARCHAR(16) NULL, 
    [OrganizationName] VARCHAR(32) NULL, 
    [MedicalStore] BIT NULL, 
    [Cell] VARCHAR(16) NULL, 
    [Dou] BIT NULL, 
    [BimaNo] VARCHAR(32) NULL, 
    [Email] VARCHAR(64) NULL, 
    [DefaultCustomer] BIT NULL, 
    [PDate] DATE NULL, 
    [RegDate] DATE NULL, 
    [Category] VARCHAR(16) NULL
)
