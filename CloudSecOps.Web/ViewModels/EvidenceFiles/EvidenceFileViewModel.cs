namespace CloudSecOps.Web.ViewModels.EvidenceFiles;

public class EvidenceFileViewModel
{
    public Guid Id { get; set; }

    public string FileName { get; set; } = string.Empty;

    public string FileType { get; set; } = string.Empty;

    public string FileUrl { get; set; } = string.Empty;

    public DateTime UploadedAt { get; set; }
}
