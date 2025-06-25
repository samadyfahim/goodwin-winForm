-- Goodwin Machine Monitoring System - Database Migration Script
-- This script adds the ImagePath column to the Machines table if it does not exist

USE GoodwinMachineMonitoring;
GO

-- Add ImagePath column if it doesn't exist
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Machines') AND name = 'ImagePath')
BEGIN
    ALTER TABLE Machines ADD ImagePath NVARCHAR(500) NULL;
    PRINT 'ImagePath column added successfully.';
END
ELSE
BEGIN
    PRINT 'ImagePath column already exists.';
END
GO 