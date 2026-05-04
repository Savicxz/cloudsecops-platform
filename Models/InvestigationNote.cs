using System.ComponentModel.DataAnnotations;

namespace CloudSecOps.Models
{
    public class InvestigationNote
    {
        public int Id { get; set; }

        public int IncidentId { get; set; }

        public Incident? Incident { get; set; }

        [Required]
        public string NoteText { get; set; } = string.Empty;

        [StringLength(100)]
        public string AddedBy { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}