using Microsoft.EntityFrameworkCore;
using goodwin_winForm.Models;

namespace goodwin_winForm.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Machine> Machines { get; set; }
        public DbSet<MaintenanceRecord> MaintenanceRecords { get; set; }
        public DbSet<MachineStatusHistory> MachineStatusHistory { get; set; }
        public DbSet<Alert> Alerts { get; set; }
        public DbSet<MachineMetrics> MachineMetrics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Machine entity
            modelBuilder.Entity<Machine>(entity =>
            {
                entity.HasKey(e => e.MachineId);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.SerialNumber).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Model).IsRequired().HasMaxLength(50);
                entity.HasIndex(e => e.SerialNumber).IsUnique();
            });

            // Configure MaintenanceRecord entity
            modelBuilder.Entity<MaintenanceRecord>(entity =>
            {
                entity.HasKey(e => e.MaintenanceId);
                entity.HasOne(e => e.Machine)
                      .WithMany(e => e.MaintenanceRecords)
                      .HasForeignKey(e => e.MachineId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure MachineStatusHistory entity
            modelBuilder.Entity<MachineStatusHistory>(entity =>
            {
                entity.HasKey(e => e.HistoryId);
                entity.HasOne(e => e.Machine)
                      .WithMany(e => e.StatusHistory)
                      .HasForeignKey(e => e.MachineId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure Alert entity
            modelBuilder.Entity<Alert>(entity =>
            {
                entity.HasKey(e => e.AlertId);
                entity.HasOne(e => e.Machine)
                      .WithMany(e => e.Alerts)
                      .HasForeignKey(e => e.MachineId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure MachineMetrics entity
            modelBuilder.Entity<MachineMetrics>(entity =>
            {
                entity.HasKey(e => e.MetricId);
                entity.HasOne(e => e.Machine)
                      .WithMany()
                      .HasForeignKey(e => e.MachineId)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasIndex(e => new { e.MachineId, e.Timestamp });
            });

            // Seed some initial data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Add sample machines
            modelBuilder.Entity<Machine>().HasData(
                new Machine
                {
                    MachineId = 1,
                    Name = "Production Line 1",
                    Description = "Main production line for widget manufacturing",
                    SerialNumber = "PL001-2024",
                    Model = "XL-2000",
                    Manufacturer = "Goodwin Industries",
                    InstallationDate = DateTime.Now.AddYears(-2),
                    Status = MachineStatus.Operational,
                    Location = "Building A",
                    Department = "Production",
                    LastMaintenanceDate = DateTime.Now.AddDays(-15),
                    NextMaintenanceDate = DateTime.Now.AddDays(15),
                    MaintenanceIntervalDays = 30,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Machine
                {
                    MachineId = 2,
                    Name = "Packaging Machine Alpha",
                    Description = "Automated packaging system",
                    SerialNumber = "PK001-2024",
                    Model = "PackMaster-500",
                    Manufacturer = "PackTech Solutions",
                    InstallationDate = DateTime.Now.AddYears(-1),
                    Status = MachineStatus.Operational,
                    Location = "Building B",
                    Department = "Packaging",
                    LastMaintenanceDate = DateTime.Now.AddDays(-10),
                    NextMaintenanceDate = DateTime.Now.AddDays(20),
                    MaintenanceIntervalDays = 30,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                }
            );

            // Add sample maintenance records
            modelBuilder.Entity<MaintenanceRecord>().HasData(
                new MaintenanceRecord
                {
                    MaintenanceId = 1,
                    MachineId = 1,
                    Type = MaintenanceType.Preventive,
                    MaintenanceDate = DateTime.Now.AddDays(-15),
                    Title = "Routine Preventive Maintenance",
                    Description = "Performed routine maintenance including oil change, filter replacement, and system calibration",
                    PerformedBy = "John Smith",
                    Cost = 250.00m,
                    PartsUsed = "Oil filter, lubricant",
                    Status = MaintenanceStatus.Completed,
                    CompletedDate = DateTime.Now.AddDays(-15),
                    CreatedAt = DateTime.Now.AddDays(-15),
                    UpdatedAt = DateTime.Now.AddDays(-15)
                },
                new MaintenanceRecord
                {
                    MaintenanceId = 2,
                    MachineId = 2,
                    Type = MaintenanceType.Inspection,
                    MaintenanceDate = DateTime.Now.AddDays(-10),
                    Title = "Monthly Inspection",
                    Description = "Monthly safety and performance inspection",
                    PerformedBy = "Jane Doe",
                    Cost = 100.00m,
                    Status = MaintenanceStatus.Completed,
                    CompletedDate = DateTime.Now.AddDays(-10),
                    CreatedAt = DateTime.Now.AddDays(-10),
                    UpdatedAt = DateTime.Now.AddDays(-10)
                }
            );
        }
    }
} 