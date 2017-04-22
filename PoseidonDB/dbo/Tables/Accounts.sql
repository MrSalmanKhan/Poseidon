CREATE TABLE [dbo].[Accounts] (
    [Id]          INT          NOT NULL IDENTITY,
    [Username]    VARCHAR (24) NULL,
    [Password]    VARCHAR (16) NULL,
    [AccountType] INT          NULL,
    CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED ([Id] ASC)
);

