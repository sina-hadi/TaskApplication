IF DB_ID('Rira') IS NULL
BEGIN
    CREATE DATABASE Rira;
    PRINT 'Database "Rira" created successfully.';
END
ELSE
BEGIN
    PRINT 'Database "Rira" already exists.';
END
GO

USE [Rira]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Task](
	[Id] [nvarchar](450) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[IsCompleted] [bit] NOT NULL,
	[DueDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Task] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE PROCEDURE [dbo].[DeleteTask]
    @Id NVARCHAR(450),
    @Result INT OUT
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM [Rira].[dbo].[Task] WHERE [Id] = @Id)
    BEGIN
        SET @Result = -1;
        RETURN;
    END

    DELETE FROM [Rira].[dbo].[Task]
    WHERE [Id] = @Id;

    SET @Result = 0;
END
GO

CREATE PROCEDURE [dbo].[InsertTask]
	@Id NVARCHAR(450),
    @Title NVARCHAR(100),
    @Description NVARCHAR(500) = NULL,
	@Result INT OUT
AS
BEGIN
	IF EXISTS (SELECT 1 FROM [Rira].[dbo].[Task] WHERE [Id] = @Id)
    BEGIN
        SET @Result = -2;
        RETURN;
    END

    INSERT INTO [Rira].[dbo].[Task] ([Id], [Title], [Description], [IsCompleted], [DueDate])
    VALUES (@Id, @Title, @Description, 0, GETDATE());

	SET @Result = 0;
END
GO

CREATE PROCEDURE [dbo].[SelectTasks]
    @Id NVARCHAR(450) = NULL,
    @Result INT OUT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM [Rira].[dbo].[Task] WHERE (@Id IS NULL OR [Id] = @Id))
    BEGIN
        SET @Result = 0;
    END
    ELSE
    BEGIN
        SET @Result = -1;
		RETURN;
    END

    SELECT 
        [Id],
        [Title],
        [Description],
        [IsCompleted],
        [DueDate]
    FROM [Rira].[dbo].[Task]
    WHERE (@Id IS NULL OR [Id] = @Id);
END
GO

CREATE PROCEDURE [dbo].[UpdateTask]
    @Id NVARCHAR(450),
    @Title NVARCHAR(100) = NULL,
    @Description NVARCHAR(500) = NULL,
    @IsCompleted BIT = NULL,
    @Result INT OUT
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM [Rira].[dbo].[Task] WHERE [Id] = @Id)
    BEGIN
        SET @Result = -1;
        RETURN;
    END

    UPDATE [Rira].[dbo].[Task]
    SET 
        [Title] = ISNULL(@Title, [Title]),
        [Description] = ISNULL(@Description, [Description]),
        [IsCompleted] = ISNULL(@IsCompleted, [IsCompleted])
    WHERE [Id] = @Id;

    SET @Result = 0;
END