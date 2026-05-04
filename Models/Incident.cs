using System.ComponentModel.DataAnnotations;

namespace CloudSecOps.Models
{
    public class Incident
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Category { get; set; } = string.Empty;

        [Required]
        [StringLength(20)]
        public string Severity { get; set; } = "Low";

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "Open";

        [Required]
        public string Description { get; set; } = string.Empty;

        [StringLength(100)]
        public string ReportedBy { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}