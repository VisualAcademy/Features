-- 특징 관리자 
CREATE TABLE [dbo].[Features]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1, 1), 
	Title NVarChar(Max) Not Null,
	Created DateTime Default(GetDate())
)
Go
