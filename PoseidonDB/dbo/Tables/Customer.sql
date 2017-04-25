

CREATE TABLE [dbo].[Customer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomerName] [varchar](64) NULL,
	[Age] [int] NULL,
	[Sex] [varchar](8) NULL,
	[Phone] [varchar](16) NULL,
	[OrganizationName] [varchar](32) NULL,
	[MedicalStore] [bit] NULL,
	[Cell] [varchar](16) NULL,
	[Dou] [bit] NULL,
	[BimaNo] [varchar](32) NULL,
	[Email] [varchar](64) NULL,
	[DefaultCustomer] [bit] NULL,
	[PDate] [date] NULL,
	[RegDate] [date] NULL,
	[Category] [varchar](16) NULL,
	[Address] [varchar](512) NULL,
	[Debit] [decimal](24, 2) NULL,
	[Credit] [decimal](24, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]




