using CloudSecOps.Web.Models.Identity;
using CloudSecOps.Web.Models.Incidents;
using CloudSecOps.Web.Models.Vulnerabilities;

namespace CloudSecOps.Web.Models.Evidence;

public class EvidenceFile
{
    public Guid Id { get; set; }

    public Guid? IncidentId { get; set; }

    public Incident? Incident { get; set; }

    public Guid? VulnerabilityId { get; set; }

    public Vulnerability? Vulnerability { get; set; }

    public string FileName { get; set; } = string.Empty;

    public string FileType { get; set; } = string.Empty;

    public string FileUrl { get; set; } = string.Empty;

    public string? UploadedByUserId { get; set; }

    public ApplicationUser? UploadedByUser { get; set; }

    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
}
