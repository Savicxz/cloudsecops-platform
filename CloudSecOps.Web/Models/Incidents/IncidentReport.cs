using System.ComponentModel.DataAnnotations;

namespace CloudSecOps.Web.Models.Incidents
{
    public class IncidentReport
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Report title is required.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Report title must be between 5 and 100 characters.")]
        [Display(Name = "Incident Title")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Category is required.")]
        [Display(Name = "Incident Category")]
        public string Category { get; set; } = string.Empty;

        [Required(ErrorMessage = "Severity level is required.")]
        [Display(Name = "Severity Level")]
        public string Severity { get; set; } = string.Empty;

        [Required(ErrorMessage = "Affected asset is required.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Affected asset must be between 3 and 100 characters.")]
        [Display(Name = "Affected Asset")]
        public string AffectedAsset { get; set; } = string.Empty;

        [Required(ErrorMessage = "Incident description is required.")]
        [StringLength(1000, MinimumLength = 20, ErrorMessage = "Description must be between 20 and 1000 characters.")]
        [Display(Name = "Incident Description")]
        public string Description { get; set; } = string.Empty;

        public string Status { get; set; } = "Draft";

        public string ReporterName { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? SubmittedAt { get; set; }

        [Display(Name = "Assigned Analyst")]
        [StringLength(100, ErrorMessage = "Assigned analyst name must not exceed 100 characters.")]
        public string? AssignedAnalyst { get; set; }

        [Display(Name = "Manager Note")]
        [StringLength(500, ErrorMessage = "Manager note must not exceed 500 characters.")]
        public string? ManagerNote { get; set; }

        public DateTime? AssignedAt { get; set; }
    }
}