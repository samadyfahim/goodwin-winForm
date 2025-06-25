-- Goodwin Machine Monitoring System - Data Seeding Script
-- This script seeds machine data and image paths

USE GoodwinMachineMonitoring;
GO

-- Update existing machines with sample image paths
UPDATE Machines 
SET ImagePath = CASE 
    WHEN MachineId = 1 THEN 'C:\GoodwinImages\Machines\production_line_1.jpg'
    WHEN MachineId = 2 THEN 'C:\GoodwinImages\Machines\packaging_machine_alpha.jpg'
    ELSE ImagePath
END
WHERE MachineId IN (1, 2);
GO

-- Enable identity insert for seeding specific IDs
SET IDENTITY_INSERT Machines ON;
GO

-- Insert additional sample machines with images
IF NOT EXISTS (SELECT * FROM Machines WHERE MachineId = 3)
BEGIN
    INSERT INTO Machines (
        MachineId, Name, Description, SerialNumber, Model, Manufacturer, 
        InstallationDate, Status, Location, Department, 
        LastMaintenanceDate, NextMaintenanceDate, MaintenanceIntervalDays, 
        Notes, ImagePath, CreatedAt, UpdatedAt
    )
    VALUES (
        3, 'CNC Milling Machine Bravo', 'Precision CNC milling machine for metal parts', 
        'CNC001-2024', 'MillMaster-3000', 'PrecisionTech Industries', 
        DATEADD(YEAR, -1, GETDATE()), 1, 'Building C', 'Manufacturing', 
        DATEADD(DAY, -5, GETDATE()), DATEADD(DAY, 25, GETDATE()), 30, 
        'High-precision machine for critical components', 
        'C:\GoodwinImages\Machines\cnc_milling_bravo.jpg', 
        GETDATE(), GETDATE()
    );
END
GO

IF NOT EXISTS (SELECT * FROM Machines WHERE MachineId = 4)
BEGIN
    INSERT INTO Machines (
        MachineId, Name, Description, SerialNumber, Model, Manufacturer, 
        InstallationDate, Status, Location, Department, 
        LastMaintenanceDate, NextMaintenanceDate, MaintenanceIntervalDays, 
        Notes, ImagePath, CreatedAt, UpdatedAt
    )
    VALUES (
        4, 'Quality Control Scanner', 'Automated quality control and inspection system', 
        'QC001-2024', 'InspectPro-500', 'QualitySystems Inc.', 
        DATEADD(MONTH, -6, GETDATE()), 1, 'Building A', 'Quality Control', 
        DATEADD(DAY, -12, GETDATE()), DATEADD(DAY, 18, GETDATE()), 30, 
        'Automated inspection system with AI capabilities', 
        'C:\GoodwinImages\Machines\quality_control_scanner.jpg', 
        GETDATE(), GETDATE()
    );
END
GO

IF NOT EXISTS (SELECT * FROM Machines WHERE MachineId = 5)
BEGIN
    INSERT INTO Machines (
        MachineId, Name, Description, SerialNumber, Model, Manufacturer, 
        InstallationDate, Status, Location, Department, 
        LastMaintenanceDate, NextMaintenanceDate, MaintenanceIntervalDays, 
        Notes, ImagePath, CreatedAt, UpdatedAt
    )
    VALUES (
        5, 'Assembly Robot Delta', 'Robotic assembly line for electronic components', 
        'ROB001-2024', 'RoboAssemble-2000', 'RoboTech Solutions', 
        DATEADD(MONTH, -3, GETDATE()), 1, 'Building B', 'Assembly', 
        DATEADD(DAY, -8, GETDATE()), DATEADD(DAY, 22, GETDATE()), 30, 
        'Advanced robotic assembly with vision system', 
        'C:\GoodwinImages\Machines\assembly_robot_delta.jpg', 
        GETDATE(), GETDATE()
    );
END
GO

-- Disable identity insert
SET IDENTITY_INSERT Machines OFF;
GO

-- Show results
SELECT MachineId, Name, SerialNumber, Model, Status, Location, ImagePath FROM Machines ORDER BY MachineId;
GO 