using System.ComponentModel.DataAnnotations;

namespace CloudSecOps.Web.Models.Incidents;

public class IncidentReport
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "Incident Title")]
    public string Title { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Incident Category")]
    public string Category { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Severity Level")]
    public string Severity { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Affected Asset")]
    public string AffectedAsset { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Incident Description")]
    public string Description { get; set; } = string.Empty;

    public string Status { get; set; } = "Open";

    public string ReporterName { get; set; } = "Reported User";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
