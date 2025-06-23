using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace goodwin_winForm.Models
{
    public class Alert
    {
        [Key]
        public int AlertId { get; set; }

        [Required]
        public int MachineId { get; set; }

        [Required]
        public AlertType Type { get; set; }

        [Required]
        public AlertSeverity Severity { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Message { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime? AcknowledgedDate { get; set; }

        [StringLength(100)]
        public string? AcknowledgedBy { get; set; }

        public DateTime? ResolvedDate { get; set; }

        [StringLength(100)]
        public string? ResolvedBy { get; set; }

        [Required]
        public AlertStatus Status { get; set; }

        [StringLength(500)]
        public string? ResolutionNotes { get; set; }

        // Navigation property
        [ForeignKey("MachineId")]
        public virtual Machine Machine { get; set; } = null!;
    }

    public enum AlertType
    {
        MaintenanceDue = 1,
        MaintenanceOverdue = 2,
        MachineFailure = 3,
        PerformanceWarning = 4,
        TemperatureWarning = 5,
        VibrationWarning = 6,
        PressureWarning = 7,
        SystemError = 8
    }

    public enum AlertSeverity
    {
        Low = 1,
        Medium = 2,
        High = 3,
        Critical = 4
    }

    public enum AlertStatus
    {
        Active = 1,
        Acknowledged = 2,
        Resolved = 3,
        Dismissed = 4
    }
} 