# Database Scripts

This folder contains all database-related SQL scripts for the Goodwin Machine Monitoring System.

## Files

### `DatabaseSetup.sql`

- **Purpose**: Initial database setup and schema creation
- **Usage**: Run this first to create the database structure
- **Contains**: Table creation scripts for Machines, MaintenanceRecords, MachineStatusHistory, and MachineMetrics

### `DatabaseMigration.sql`

- **Purpose**: Database migration to add ImagePath column
- **Usage**: Run after DatabaseSetup.sql to add image support
- **Contains**: ALTER TABLE script to add ImagePath column to Machines table

### `SeedMachinesWithImages.sql`

- **Purpose**: Seed initial sample data with image paths
- **Usage**: Run after migrations to populate with sample machines
- **Contains**: INSERT statements for machines 1-2 with image paths

### `AddMoreMachines.sql`

- **Purpose**: Add additional sample machines with comprehensive data
- **Usage**: Run after initial seeding to add more machines
- **Contains**: INSERT statements for machines 3-5 with maintenance records

## Execution Order

1. `DatabaseSetup.sql` - Create database structure
2. `DatabaseMigration.sql` - Add image support
3. `SeedMachinesWithImages.sql` - Add initial sample data
4. `AddMoreMachines.sql` - Add additional sample data

## Notes

- All scripts use the `GoodwinMachineMonitoring` database
- Scripts include safety checks to prevent duplicate data
- Image paths reference `C:\GoodwinImages\Machines\` directory
- Use SQL Server Management Studio or sqlcmd to execute these scripts
