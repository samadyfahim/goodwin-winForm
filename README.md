# Goodwin Machine Monitoring System

A Windows Forms application for monitoring machine status and tracking maintenance activities.

## Demo

https://github.com/user-attachments/assets/412d89eb-08e0-43f9-8701-ebe74e25bd76

## Database Structure

The application uses Entity Framework Core with SQL Server to manage the following entities:

### Core Entities

#### 1. Machine

- **Purpose**: Represents physical machines in the system
- **Key Fields**:
  - `MachineId` (Primary Key)
  - `Name`, `Description`, `SerialNumber`, `Model`, `Manufacturer`
  - `InstallationDate`, `Status`, `Location`, `Department`
  - `LastMaintenanceDate`, `NextMaintenanceDate`, `MaintenanceIntervalDays`
  - `CreatedAt`, `UpdatedAt`

#### 2. MaintenanceRecord

- **Purpose**: Tracks all maintenance activities performed on machines
- **Key Fields**:
  - `MaintenanceId` (Primary Key)
  - `MachineId` (Foreign Key)
  - `Type` (Preventive, Corrective, Emergency, Inspection, Calibration)
  - `MaintenanceDate`, `Title`, `Description`, `PerformedBy`
  - `Cost`, `PartsUsed`, `Status`, `CompletedDate`

#### 3. MachineStatusHistory

- **Purpose**: Audit trail for machine status changes
- **Key Fields**:
  - `HistoryId` (Primary Key)
  - `MachineId` (Foreign Key)
  - `OldStatus`, `NewStatus`, `StatusChangeDate`
  - `Reason`, `ChangedBy`, `Notes`

#### 4. Alert

- **Purpose**: System alerts and notifications
- **Key Fields**:
  - `AlertId` (Primary Key)
  - `MachineId` (Foreign Key)
  - `Type`, `Severity`, `Title`, `Message`
  - `CreatedDate`, `AcknowledgedDate`, `ResolvedDate`
  - `Status` (Active, Acknowledged, Resolved, Dismissed)

#### 5. MachineMetrics

- **Purpose**: Real-time monitoring data
- **Key Fields**:
  - `MetricId` (Primary Key)
  - `MachineId` (Foreign Key)
  - `Timestamp`, `Temperature`, `Vibration`, `Pressure`
  - `Speed`, `PowerConsumption`, `Efficiency`, `Uptime`, `Downtime`

### Enums

#### MachineStatus

- `Operational` = 1
- `UnderMaintenance` = 2
- `OutOfService` = 3
- `Warning` = 4
- `Critical` = 5

#### MaintenanceType

- `Preventive` = 1
- `Corrective` = 2
- `Emergency` = 3
- `Inspection` = 4
- `Calibration` = 5

#### AlertSeverity

- `Low` = 1
- `Medium` = 2
- `High` = 3
- `Critical` = 4

## Setup Instructions

### Prerequisites

- .NET 9.0 SDK
- SQL Server (LocalDB recommended for development)
- Visual Studio 2022 or later

### Database Setup

1. The application will automatically create the database on first run
2. Default connection string: `Server=(localdb)\mssqllocaldb;Database=GoodwinMachineMonitoring;Trusted_Connection=true;MultipleActiveResultSets=true`
3. Sample data is automatically seeded on database creation

### Running the Application

1. Build the solution
2. Run the application
3. The database will be initialized automatically on first run

## Features

- **Machine Management**: Add, edit, and delete machines
- **Maintenance Tracking**: Record and track maintenance activities
- **Status Monitoring**: Monitor machine status in real-time
- **Alert System**: Automated alerts for maintenance due, failures, etc.
- **Metrics Collection**: Store and analyze machine performance data
- **Audit Trail**: Complete history of status changes and maintenance activities

## Architecture

- **Models**: Entity classes with data annotations
- **Services**: Repository pattern for data access
- **Forms**: Windows Forms UI components
- **Database**: Entity Framework Core with SQL Server

## Dependencies

- Microsoft.EntityFrameworkCore (9.0.0)
- Microsoft.EntityFrameworkCore.SqlServer (9.0.0)
- Microsoft.EntityFrameworkCore.Tools (9.0.0)
- Microsoft.EntityFrameworkCore.Design (9.0.0)
- Microsoft.Extensions.Hosting (9.0.0)
- Microsoft.Extensions.DependencyInjection (9.0.0)
