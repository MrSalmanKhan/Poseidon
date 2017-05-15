CREATE TABLE [dbo].[BankAccount](
Id INT NOT NULL PRIMARY KEY IDENTITY,
 [TimeStamp] DATETIME NOT NULL,
 [AccountTitle] varchar(64),
 [BankName] varchar(64),
 [Balance] Decimal(24,2)
)