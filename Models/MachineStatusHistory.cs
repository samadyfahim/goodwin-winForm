using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace goodwin_winForm.Models
{
    public class MachineStatusHistory
    {
        [Key]
        public int HistoryId { get; set; }

        [Required]
        public int MachineId { get; set; }

        [Required]
        public MachineStatus OldStatus { get; set; }

        [Required]
        public MachineStatus NewStatus { get; set; }

        [Required]
        public DateTime StatusChangeDate { get; set; }

        [StringLength(200)]
        public string? Reason { get; set; }

        [StringLength(100)]
        public string? ChangedBy { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }

        // Navigation property
        [ForeignKey("MachineId")]
        public virtual Machine Machine { get; set; } = null!;
    }
} 