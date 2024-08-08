USE master

IF NOT EXISTS (
    SELECT name 
    FROM [sys].[databases]
    WHERE [name] = N'SanaTest'
)
BEGIN
    CREATE DATABASE [SanaTest]
END
GO

IF EXISTS (
    SELECT name 
    FROM [sys].[databases]
    WHERE [name] = N'SanaTest'
)
BEGIN
    USE [SanaTest]
END
GO

IF NOT EXISTS (SELECT TOP 1 1
			   FROM [INFORMATION_SCHEMA].[SCHEMATA]
			   WHERE [CATALOG_NAME] = 'SanaTest'
			   AND [SCHEMA_NAME] = 'shop')
BEGIN
	EXEC('CREATE SCHEMA [shop]')
END
GO

IF NOT EXISTS (
	SELECT TOP 1 1 
	FROM [INFORMATION_SCHEMA].[TABLES]
	WHERE [TABLE_CATALOG] = 'SanaTest'
	AND [TABLE_SCHEMA] = 'shop'
	AND [TABLE_NAME] = 'categories'
)
BEGIN
	CREATE TABLE [shop].[categories] 
	(
		 [Id] INT NOT NULL PRIMARY KEY IDENTITY
		,[Name] NVARCHAR(50) NOT NULL UNIQUE
	)
END
GO

IF NOT EXISTS (
	SELECT TOP 1 1 
	FROM [INFORMATION_SCHEMA].[TABLES]
	WHERE [TABLE_CATALOG] = 'SanaTest'
	AND [TABLE_SCHEMA] = 'shop'
	AND [TABLE_NAME] = 'products'
)
BEGIN
	CREATE TABLE [shop].[products] 
	(
		 [Id] INT NOT NULL PRIMARY KEY IDENTITY
		,[Code] NVARCHAR(50) NOT NULL UNIQUE
		,[Name] NVARCHAR(50) NOT NULL UNIQUE
		,[Description] NVARCHAR(250) NOT NULL
		,[Price] DECIMAL NOT NULL
		,[Stock] DECIMAL NOT NULL
		,[Img] NVARCHAR(MAX) NOT NULL
	)
END
GO

IF NOT EXISTS (
	SELECT TOP 1 1 
	FROM [INFORMATION_SCHEMA].[TABLES]
	WHERE [TABLE_CATALOG] = 'SanaTest'
	AND [TABLE_SCHEMA] = 'shop'
	AND [TABLE_NAME] = 'product_categories'
)
BEGIN
	CREATE TABLE [shop].[product_categories] 
	(
		 [Id] INT NOT NULL PRIMARY KEY IDENTITY
		,[Id_product] INT NOT NULL FOREIGN KEY REFERENCES [shop].[products]([Id])
		,[Id_categoy] INT NOT NULL FOREIGN KEY REFERENCES [shop].[categories]([Id])
	)
END
GO

IF NOT EXISTS (
	SELECT TOP 1 1 
	FROM [INFORMATION_SCHEMA].[TABLES]
	WHERE [TABLE_CATALOG] = 'SanaTest'
	AND [TABLE_SCHEMA] = 'shop'
	AND [TABLE_NAME] = 'customers'
)
BEGIN
	CREATE TABLE [shop].[customers] 
	(
		 [Id] INT NOT NULL PRIMARY KEY IDENTITY
		,[Code] UNIQUEIDENTIFIER UNIQUE NOT NULL
		,[Name] NVARCHAR(250) NOT NULL
		,[Email] NVARCHAR(250) NOT NULL
	)
END
GO

IF NOT EXISTS (
	SELECT TOP 1 1 
	FROM [INFORMATION_SCHEMA].[TABLES]
	WHERE [TABLE_CATALOG] = 'SanaTest'
	AND [TABLE_SCHEMA] = 'shop'
	AND [TABLE_NAME] = 'orders'
)
BEGIN
	CREATE TABLE [shop].[orders] 
	(
		  [Id] INT NOT NULL PRIMARY KEY IDENTITY
		 ,[Id_customer] INT NOT NULL FOREIGN KEY REFERENCES [shop].[customers]([Id])
		 ,[Total_products] INT NOT NULL
		 ,[Total_price] DECIMAL NOT NULL
	)
END
GO

IF NOT EXISTS (
	SELECT TOP 1 1 
	FROM [INFORMATION_SCHEMA].[TABLES]
	WHERE [TABLE_CATALOG] = 'SanaTest'
	AND [TABLE_SCHEMA] = 'shop'
	AND [TABLE_NAME] = 'order_products'
)
BEGIN
	CREATE TABLE [shop].[order_products] 
	(
		 [Id] INT NOT NULL PRIMARY KEY IDENTITY
		,[Id_order] INT NOT NULL FOREIGN KEY REFERENCES [shop].[orders]([Id])
		,[Id_product] INT NOT NULL FOREIGN KEY REFERENCES [shop].[products]([Id])
	)
END
GO

CREATE OR ALTER PROCEDURE [shop].[SP_Get_Products]
AS
BEGIN 
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
	SET NOCOUNT ON;
	SELECT   [Id]
			,[Code]
			,[Name]
			,[Description]
			,[Price]
			,[Stock]
			,[Img]
			,(
				SELECT	 C.[Id]
						,C.[Name]
				FROM [shop].[product_categories] PC
				INNER JOIN [shop].[categories] C
					ON PC.[Id_product] = P.[Id]
					AND PC.[Id_categoy] = C.[Id]
				FOR JSON PATH
			) AS [Categories]
	FROM [shop].[products] P
END
GO