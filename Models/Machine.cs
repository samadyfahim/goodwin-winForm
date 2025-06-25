using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace goodwin_winForm.Models
{
    public class Machine
    {
        [Key]
        public int MachineId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(200)]
        public string? Description { get; set; }

        [Required]
        [StringLength(50)]
        public string SerialNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Model { get; set; } = string.Empty;

        [StringLength(100)]
        public string? Manufacturer { get; set; }

        [Required]
        public DateTime InstallationDate { get; set; }

        [Required]
        public MachineStatus Status { get; set; }

        [StringLength(100)]
        public string? Location { get; set; }

        [StringLength(100)]
        public string? Department { get; set; }

        public DateTime LastMaintenanceDate { get; set; }

        public DateTime NextMaintenanceDate { get; set; }

        public int MaintenanceIntervalDays { get; set; } = 30;

        [StringLength(500)]
        public string? Notes { get; set; }

        [StringLength(500)]
        public string? ImagePath { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Navigation properties
        public virtual ICollection<MaintenanceRecord> MaintenanceRecords { get; set; } = new List<MaintenanceRecord>();
        public virtual ICollection<MachineStatusHistory> StatusHistory { get; set; } = new List<MachineStatusHistory>();
        public virtual ICollection<Alert> Alerts { get; set; } = new List<Alert>();
    }

    public enum MachineStatus
    {
        Operational = 1,
        UnderMaintenance = 2,
        OutOfService = 3,
        Warning = 4,
        Critical = 5
    }
} 