-- Add more machines with image paths
USE GoodwinMachineMonitoring;
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

-- Insert sample maintenance records for new machines
IF NOT EXISTS (SELECT * FROM MaintenanceRecords WHERE MaintenanceId = 3)
BEGIN
    INSERT INTO MaintenanceRecords (MaintenanceId, MachineId, Type, MaintenanceDate, Title, Description, PerformedBy, Cost, PartsUsed, Status, CompletedDate, CreatedAt, UpdatedAt)
    VALUES (3, 3, 1, DATEADD(DAY, -5, GETDATE()), 'Preventive Maintenance', 'Routine maintenance including calibration and lubrication', 'Mike Johnson', 300.00, 'Lubricant, calibration tools', 3, DATEADD(DAY, -5, GETDATE()), DATEADD(DAY, -5, GETDATE()), DATEADD(DAY, -5, GETDATE()));
END
GO

IF NOT EXISTS (SELECT * FROM MaintenanceRecords WHERE MaintenanceId = 4)
BEGIN
    INSERT INTO MaintenanceRecords (MaintenanceId, MachineId, Type, MaintenanceDate, Title, Description, PerformedBy, Cost, Status, CompletedDate, CreatedAt, UpdatedAt)
    VALUES (4, 4, 2, DATEADD(DAY, -12, GETDATE()), 'Software Update', 'Updated inspection software and recalibrated sensors', 'Sarah Wilson', 150.00, 3, DATEADD(DAY, -12, GETDATE()), DATEADD(DAY, -12, GETDATE()), DATEADD(DAY, -12, GETDATE()));
END
GO

IF NOT EXISTS (SELECT * FROM MaintenanceRecords WHERE MaintenanceId = 5)
BEGIN
    INSERT INTO MaintenanceRecords (MaintenanceId, MachineId, Type, MaintenanceDate, Title, Description, PerformedBy, Cost, PartsUsed, Status, CompletedDate, CreatedAt, UpdatedAt)
    VALUES (5, 5, 1, DATEADD(DAY, -8, GETDATE()), 'Robot Maintenance', 'Serviced robotic arms and updated control software', 'David Chen', 450.00, 'Servo motors, control software', 3, DATEADD(DAY, -8, GETDATE()), DATEADD(DAY, -8, GETDATE()), DATEADD(DAY, -8, GETDATE()));
END
GO

-- Verify the results
SELECT 
    MachineId, 
    Name, 
    SerialNumber, 
    Model, 
    Status, 
    Location,
    ImagePath,
    CASE 
        WHEN ImagePath IS NOT NULL THEN 'Has Image'
        ELSE 'No Image'
    END as ImageStatus
FROM Machines
ORDER BY MachineId;
GO

PRINT 'Additional machines added successfully!';
PRINT 'Total machines in database: ' + CAST((SELECT COUNT(*) FROM Machines) AS NVARCHAR(10));
PRINT 'Machines with images: ' + CAST((SELECT COUNT(*) FROM Machines WHERE ImagePath IS NOT NULL) AS NVARCHAR(10));
GO 