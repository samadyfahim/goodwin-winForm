using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace goodwin_winForm.Models
{
    public class MachineMetrics
    {
        [Key]
        public int MetricId { get; set; }

        [Required]
        public int MachineId { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

        public decimal? Temperature { get; set; }

        public decimal? Vibration { get; set; }

        public decimal? Pressure { get; set; }

        public decimal? Speed { get; set; }

        public decimal? PowerConsumption { get; set; }

        public decimal? Efficiency { get; set; }

        public decimal? Uptime { get; set; }

        public decimal? Downtime { get; set; }

        [StringLength(50)]
        public string? Unit { get; set; }

        [StringLength(200)]
        public string? Notes { get; set; }

        // Navigation property
        [ForeignKey("MachineId")]
        public virtual Machine Machine { get; set; } = null!;
    }
} 