-- Goodwin Machine Monitoring System Database Setup Script
-- This script can be used to manually set up the database if Entity Framework migration is not used

USE master;
GO

-- Create the database if it doesn't exist
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'GoodwinMachineMonitoring')
BEGIN
    CREATE DATABASE GoodwinMachineMonitoring;
END
GO

USE GoodwinMachineMonitoring;
GO

-- Create Machines table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Machines')
BEGIN
    CREATE TABLE Machines (
        MachineId INT IDENTITY(1,1) PRIMARY KEY,
        Name NVARCHAR(100) NOT NULL,
        Description NVARCHAR(200) NULL,
        SerialNumber NVARCHAR(50) NOT NULL UNIQUE,
        Model NVARCHAR(50) NOT NULL,
        Manufacturer NVARCHAR(100) NULL,
        InstallationDate DATETIME2 NOT NULL,
        Status INT NOT NULL,
        Location NVARCHAR(100) NULL,
        Department NVARCHAR(100) NULL,
        LastMaintenanceDate DATETIME2 NOT NULL,
        NextMaintenanceDate DATETIME2 NOT NULL,
        MaintenanceIntervalDays INT NOT NULL DEFAULT 30,
        Notes NVARCHAR(500) NULL,
        ImagePath NVARCHAR(500) NULL,
        CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
        UpdatedAt DATETIME2 NOT NULL DEFAULT GETDATE()
    );
END
GO

-- Add ImagePath column to existing Machines table if it doesn't exist
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Machines') AND name = 'ImagePath')
BEGIN
    ALTER TABLE Machines ADD ImagePath NVARCHAR(500) NULL;
END
GO

-- Create MaintenanceRecords table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'MaintenanceRecords')
BEGIN
    CREATE TABLE MaintenanceRecords (
        MaintenanceId INT IDENTITY(1,1) PRIMARY KEY,
        MachineId INT NOT NULL,
        Type INT NOT NULL,
        MaintenanceDate DATETIME2 NOT NULL,
        Title NVARCHAR(200) NOT NULL,
        Description NVARCHAR(1000) NULL,
        PerformedBy NVARCHAR(100) NOT NULL,
        Cost DECIMAL(10,2) NOT NULL DEFAULT 0,
        PartsUsed NVARCHAR(100) NULL,
        Status INT NOT NULL,
        CompletedDate DATETIME2 NULL,
        Notes NVARCHAR(500) NULL,
        CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
        UpdatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
        FOREIGN KEY (MachineId) REFERENCES Machines(MachineId) ON DELETE CASCADE
    );
END
GO

-- Create MachineStatusHistory table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'MachineStatusHistory')
BEGIN
    CREATE TABLE MachineStatusHistory (
        HistoryId INT IDENTITY(1,1) PRIMARY KEY,
        MachineId INT NOT NULL,
        OldStatus INT NOT NULL,
        NewStatus INT NOT NULL,
        StatusChangeDate DATETIME2 NOT NULL,
        Reason NVARCHAR(200) NULL,
        ChangedBy NVARCHAR(100) NULL,
        Notes NVARCHAR(500) NULL,
        FOREIGN KEY (MachineId) REFERENCES Machines(MachineId) ON DELETE CASCADE
    );
END
GO

-- Create Alerts table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Alerts')
BEGIN
    CREATE TABLE Alerts (
        AlertId INT IDENTITY(1,1) PRIMARY KEY,
        MachineId INT NOT NULL,
        Type INT NOT NULL,
        Severity INT NOT NULL,
        Title NVARCHAR(200) NOT NULL,
        Message NVARCHAR(1000) NULL,
        CreatedDate DATETIME2 NOT NULL DEFAULT GETDATE(),
        AcknowledgedDate DATETIME2 NULL,
        AcknowledgedBy NVARCHAR(100) NULL,
        ResolvedDate DATETIME2 NULL,
        ResolvedBy NVARCHAR(100) NULL,
        Status INT NOT NULL,
        ResolutionNotes NVARCHAR(500) NULL,
        FOREIGN KEY (MachineId) REFERENCES Machines(MachineId) ON DELETE CASCADE
    );
END
GO

-- Create MachineMetrics table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'MachineMetrics')
BEGIN
    CREATE TABLE MachineMetrics (
        MetricId INT IDENTITY(1,1) PRIMARY KEY,
        MachineId INT NOT NULL,
        Timestamp DATETIME2 NOT NULL,
        Temperature DECIMAL(10,2) NULL,
        Vibration DECIMAL(10,2) NULL,
        Pressure DECIMAL(10,2) NULL,
        Speed DECIMAL(10,2) NULL,
        PowerConsumption DECIMAL(10,2) NULL,
        Efficiency DECIMAL(10,2) NULL,
        Uptime DECIMAL(10,2) NULL,
        Downtime DECIMAL(10,2) NULL,
        Unit NVARCHAR(50) NULL,
        Notes NVARCHAR(200) NULL,
        FOREIGN KEY (MachineId) REFERENCES Machines(MachineId) ON DELETE CASCADE
    );
END
GO

-- Create indexes for better performance
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Machines_SerialNumber')
BEGIN
    CREATE UNIQUE INDEX IX_Machines_SerialNumber ON Machines(SerialNumber);
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_MaintenanceRecords_MachineId')
BEGIN
    CREATE INDEX IX_MaintenanceRecords_MachineId ON MaintenanceRecords(MachineId);
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_MaintenanceRecords_MaintenanceDate')
BEGIN
    CREATE INDEX IX_MaintenanceRecords_MaintenanceDate ON MaintenanceRecords(MaintenanceDate);
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Alerts_MachineId')
BEGIN
    CREATE INDEX IX_Alerts_MachineId ON Alerts(MachineId);
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Alerts_Status')
BEGIN
    CREATE INDEX IX_Alerts_Status ON Alerts(Status);
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Alerts_CreatedDate')
BEGIN
    CREATE INDEX IX_Alerts_CreatedDate ON Alerts(CreatedDate);
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_MachineMetrics_MachineId_Timestamp')
BEGIN
    CREATE INDEX IX_MachineMetrics_MachineId_Timestamp ON MachineMetrics(MachineId, Timestamp);
END
GO

-- Insert sample data
IF NOT EXISTS (SELECT * FROM Machines WHERE MachineId = 1)
BEGIN
    INSERT INTO Machines (MachineId, Name, Description, SerialNumber, Model, Manufacturer, InstallationDate, Status, Location, Department, LastMaintenanceDate, NextMaintenanceDate, MaintenanceIntervalDays, CreatedAt, UpdatedAt)
    VALUES (1, 'Production Line 1', 'Main production line for widget manufacturing', 'PL001-2024', 'XL-2000', 'Goodwin Industries', DATEADD(YEAR, -2, GETDATE()), 1, 'Building A', 'Production', DATEADD(DAY, -15, GETDATE()), DATEADD(DAY, 15, GETDATE()), 30, GETDATE(), GETDATE());
END
GO

IF NOT EXISTS (SELECT * FROM Machines WHERE MachineId = 2)
BEGIN
    INSERT INTO Machines (MachineId, Name, Description, SerialNumber, Model, Manufacturer, InstallationDate, Status, Location, Department, LastMaintenanceDate, NextMaintenanceDate, MaintenanceIntervalDays, CreatedAt, UpdatedAt)
    VALUES (2, 'Packaging Machine Alpha', 'Automated packaging system', 'PK001-2024', 'PackMaster-500', 'PackTech Solutions', DATEADD(YEAR, -1, GETDATE()), 1, 'Building B', 'Packaging', DATEADD(DAY, -10, GETDATE()), DATEADD(DAY, 20, GETDATE()), 30, GETDATE(), GETDATE());
END
GO

-- Insert sample maintenance records
IF NOT EXISTS (SELECT * FROM MaintenanceRecords WHERE MaintenanceId = 1)
BEGIN
    INSERT INTO MaintenanceRecords (MaintenanceId, MachineId, Type, MaintenanceDate, Title, Description, PerformedBy, Cost, PartsUsed, Status, CompletedDate, CreatedAt, UpdatedAt)
    VALUES (1, 1, 1, DATEADD(DAY, -15, GETDATE()), 'Routine Preventive Maintenance', 'Performed routine maintenance including oil change, filter replacement, and system calibration', 'John Smith', 250.00, 'Oil filter, lubricant', 3, DATEADD(DAY, -15, GETDATE()), DATEADD(DAY, -15, GETDATE()), DATEADD(DAY, -15, GETDATE()));
END
GO

IF NOT EXISTS (SELECT * FROM MaintenanceRecords WHERE MaintenanceId = 2)
BEGIN
    INSERT INTO MaintenanceRecords (MaintenanceId, MachineId, Type, MaintenanceDate, Title, Description, PerformedBy, Cost, Status, CompletedDate, CreatedAt, UpdatedAt)
    VALUES (2, 2, 4, DATEADD(DAY, -10, GETDATE()), 'Monthly Inspection', 'Monthly safety and performance inspection', 'Jane Doe', 100.00, 3, DATEADD(DAY, -10, GETDATE()), DATEADD(DAY, -10, GETDATE()), DATEADD(DAY, -10, GETDATE()));
END
GO

PRINT 'Database setup completed successfully!';
GO 