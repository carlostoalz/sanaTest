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
		,[Quantity] INT NOT NULL
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

CREATE OR ALTER PROCEDURE [shop].[SP_Create_Order]
(
	@order_info NVARCHAR(MAX)
)
AS
BEGIN
	SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
	SET NOCOUNT ON;
	DECLARE @IdCustomer INT,
			@IdOrder INT,
			@ErrorMessage NVARCHAR(MAX),
			@ErrorState INT;
	BEGIN TRY
		BEGIN TRAN
			IF NOT EXISTS (
				SELECT TOP 1 1
				FROM OPENJSON(@order_info, '$.customer') WITH (
					 [Id] INT '$.id'
					,[Code] UNIQUEIDENTIFIER '$.code'
					,[Name] NVARCHAR(250) '$.name'
					,[Email] NVARCHAR(250) '$.email'
				) A
				INNER JOIN [shop].[customers] B
					ON A.[Email] = B.[Email]
			)
			BEGIN
				INSERT INTO [shop].[customers]
				(
					 [Code]
					,[Name]
					,[Email]
				)
				SELECT	 [Code]
						,[Name]
						,[Email]
				FROM OPENJSON(@order_info, '$.customer') WITH (
					 [Id] INT '$.id'
					,[Code] UNIQUEIDENTIFIER '$.code'
					,[Name] NVARCHAR(250) '$.name'
					,[Email] NVARCHAR(250) '$.email'
				) A
				SET @IdCustomer = SCOPE_IDENTITY()
			END
			ELSE 
			BEGIN 
				SELECT TOP 1 @IdCustomer = B.[Id]
				FROM OPENJSON(@order_info, '$.customer') WITH (
					 [Id] INT '$.id'
					,[Code] UNIQUEIDENTIFIER '$.code'
					,[Name] NVARCHAR(250) '$.name'
					,[Email] NVARCHAR(250) '$.email'
				) A
				INNER JOIN [shop].[customers] B
					ON A.[Email] = B.[Email]
			END

			UPDATE A
				SET A.[Stock] = A.[Stock] - B.[Quantity]
			FROM [shop].[products] A
			INNER JOIN OPENJSON(@order_info, '$.orderProducts') WITH (
				 [Id] INT '$.id'
				,[Id_product] INT '$.id_product'
				,[Quantity] INT '$.quantity'
			) B
				ON A.[Id] = B.[Id_product]

			INSERT INTO [shop].[orders]
			(
				 [Id_customer]
				,[Total_products]
				,[Total_price]
			)
			SELECT	 @IdCustomer
					,[Total_products]
					,[Total_price]
			FROM OPENJSON(@order_info, '$.order') WITH (
				 [Total_products] INT  '$.total_products'
				,[Total_price] DECIMAL '$.total_price'
			)

			SET @IdOrder = SCOPE_IDENTITY()

			INSERT INTO [shop].[order_products]
			(
				 [Id_order]
				,[Id_product]
				,[Quantity]
			)
			SELECT	 @IdOrder
					,[Id_product]
					,[Quantity]
			FROM OPENJSON(@order_info, '$.orderProducts') WITH (
				 [Id_product] INT '$.id_product'
				,[Quantity] INT '$.quantity'
			)
		COMMIT TRAN
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
			ROLLBACK TRANSACTION;
		SET @ErrorMessage = 'Error en el procedimiento [shop].[SP_Create_Order]: ' + ERROR_MESSAGE();
		SET @ErrorState = ERROR_STATE();
		THROW 50000, @ErrorMessage, @ErrorState
	END CATCH
END
GO