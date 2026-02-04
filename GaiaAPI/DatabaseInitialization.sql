-- A34D - Gaia Project Database Initialization Script
-- This script creates the database and tables manually if needed

USE master;
GO

-- Create Database
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'GaiaProjectDb')
BEGIN
    CREATE DATABASE GaiaProjectDb;
    PRINT 'Database GaiaProjectDb created successfully';
END
ELSE
BEGIN
    PRINT 'Database GaiaProjectDb already exists';
END
GO

USE GaiaProjectDb;
GO

-- Create Operations Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Operations')
BEGIN
    CREATE TABLE Operations (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Name NVARCHAR(100) NOT NULL,
        DisplayName NVARCHAR(200) NOT NULL,
        Description NVARCHAR(500) NULL,
        OperationType NVARCHAR(50) NOT NULL,
        ImplementationClass NVARCHAR(200) NOT NULL,
        IsActive BIT NOT NULL DEFAULT 1,
        SortOrder INT NOT NULL,
        CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        UpdatedAt DATETIME2 NULL
    );

    -- Unique index on Name
    CREATE UNIQUE NONCLUSTERED INDEX IX_Operations_Name 
    ON Operations(Name);

    -- Create indexes for performance
    CREATE NONCLUSTERED INDEX IX_Operations_OperationType ON Operations(OperationType);
    CREATE NONCLUSTERED INDEX IX_Operations_IsActive ON Operations(IsActive);

    PRINT 'Operations table created successfully';
END
ELSE
BEGIN
    PRINT 'Operations table already exists';
END
GO

-- Create OperationHistories Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'OperationHistories')
BEGIN
    CREATE TABLE OperationHistories (
        Id INT PRIMARY KEY IDENTITY(1,1),
        OperationId INT NOT NULL,
        FieldA NVARCHAR(1000) NOT NULL,
        FieldB NVARCHAR(1000) NOT NULL,
        Result NVARCHAR(2000) NULL,
        IsSuccess BIT NOT NULL,
        ErrorMessage NVARCHAR(2000) NULL,
        ExecutedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        ClientInfo NVARCHAR(500) NULL,
        CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
        UpdatedAt DATETIME2 NULL,
        CONSTRAINT FK_OperationHistories_Operations FOREIGN KEY (OperationId) 
            REFERENCES Operations(Id)
    );

    -- Create indexes for performance
    CREATE NONCLUSTERED INDEX IX_OperationHistories_OperationId ON OperationHistories(OperationId);
    CREATE NONCLUSTERED INDEX IX_OperationHistories_ExecutedAt ON OperationHistories(ExecutedAt);
    CREATE NONCLUSTERED INDEX IX_OperationHistories_IsSuccess ON OperationHistories(IsSuccess);

    PRINT 'OperationHistories table created successfully';
END
ELSE
BEGIN
    PRINT 'OperationHistories table already exists';
END
GO

-- Insert initial operations (seed data)
IF NOT EXISTS (SELECT * FROM Operations)
BEGIN
    INSERT INTO Operations (Name, DisplayName, Description, OperationType, ImplementationClass, IsActive, SortOrder, CreatedAt)
    VALUES 
        ('Addition', 'Addition (+)', 'Adds two numbers together', 'Arithmetic', 'AdditionOperationExecutor', 1, 1, GETUTCDATE()),
        ('Subtraction', 'Subtraction (-)', 'Subtracts the second number from the first', 'Arithmetic', 'SubtractionOperationExecutor', 1, 2, GETUTCDATE()),
        ('Multiplication', 'Multiplication (×)', 'Multiplies two numbers', 'Arithmetic', 'MultiplicationOperationExecutor', 1, 3, GETUTCDATE()),
        ('Division', 'Division (÷)', 'Divides the first number by the second', 'Arithmetic', 'DivisionOperationExecutor', 1, 4, GETUTCDATE()),
        ('Modulo', 'Modulo (%)', 'Returns the remainder of division', 'Arithmetic', 'ModuloOperationExecutor', 1, 5, GETUTCDATE()),
        ('Power', 'Power (^)', 'Raises the first number to the power of the second', 'Arithmetic', 'PowerOperationExecutor', 1, 6, GETUTCDATE()),
        ('Concatenation', 'Concatenation (String)', 'Joins two strings together', 'String', 'ConcatenationOperationExecutor', 1, 7, GETUTCDATE()),
        ('Compare', 'Compare (String)', 'Compares two strings', 'String', 'CompareOperationExecutor', 1, 8, GETUTCDATE()),
        ('Contains', 'Contains (String)', 'Checks if first string contains the second', 'String', 'ContainsOperationExecutor', 1, 9, GETUTCDATE()),
        ('LengthCompare', 'Length Compare (String)', 'Compares the lengths of two strings', 'String', 'LengthCompareOperationExecutor', 1, 10, GETUTCDATE());

    PRINT 'Initial operations seeded successfully';
END
ELSE
BEGIN
    PRINT 'Operations already exist, skipping seed';
END
GO

-- Verify the setup
SELECT 'Database Setup Verification' AS Status;
SELECT COUNT(*) AS OperationsCount FROM Operations;
SELECT COUNT(*) AS HistoriesCount FROM OperationHistories;
GO

PRINT 'Database initialization completed successfully - A34D';
GO
