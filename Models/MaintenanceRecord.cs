using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace goodwin_winForm.Models
{
    public class MaintenanceRecord
    {
        [Key]
        public int MaintenanceId { get; set; }

        [Required]
        public int MachineId { get; set; }

        [Required]
        public MaintenanceType Type { get; set; }

        [Required]
        public DateTime MaintenanceDate { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Description { get; set; }

        [Required]
        [StringLength(100)]
        public string PerformedBy { get; set; } = string.Empty;

        public decimal Cost { get; set; }

        [StringLength(100)]
        public string? PartsUsed { get; set; }

        [Required]
        public MaintenanceStatus Status { get; set; }

        public DateTime? CompletedDate { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Navigation property
        [ForeignKey("MachineId")]
        public virtual Machine Machine { get; set; } = null!;
    }

    public enum MaintenanceType
    {
        Preventive = 1,
        Corrective = 2,
        Emergency = 3,
        Inspection = 4,
        Calibration = 5
    }

    public enum MaintenanceStatus
    {
        Scheduled = 1,
        InProgress = 2,
        Completed = 3,
        Cancelled = 4,
        Overdue = 5
    }
} 