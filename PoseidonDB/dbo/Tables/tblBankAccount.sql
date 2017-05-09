Create table tblBankAccount
(Id int Primary key Identity(1,1),
 TimeStamp datetime not null,
 AccountTitle varchar(64),
 BankName varchar(64),
 Balance Decimal(24,2)
)