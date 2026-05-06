using CloudSecOps.Web.Models.Assets;
using CloudSecOps.Web.Models.Audit;
using CloudSecOps.Web.Models.Evidence;
using CloudSecOps.Web.Models.Identity;
using CloudSecOps.Web.Models.Incidents;
using CloudSecOps.Web.Models.Vulnerabilities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CloudSecOps.Web.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Asset> Assets => Set<Asset>();

    public DbSet<Incident> Incidents => Set<Incident>();

    public DbSet<IncidentReport> IncidentReports => Set<IncidentReport>();

    public DbSet<IncidentUpdate> IncidentUpdates => Set<IncidentUpdate>();

    public DbSet<Vulnerability> Vulnerabilities => Set<Vulnerability>();

    public DbSet<VulnerabilityUpdate> VulnerabilityUpdates => Set<VulnerabilityUpdate>();

    public DbSet<EvidenceFile> EvidenceFiles => Set<EvidenceFile>();

    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Incident>()
            .HasOne(i => i.RelatedAsset)
            .WithMany()
            .HasForeignKey(i => i.RelatedAssetId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Entity<Incident>()
            .HasOne(i => i.ReportedByUser)
            .WithMany()
            .HasForeignKey(i => i.ReportedByUserId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Entity<Incident>()
            .HasOne(i => i.AssignedToUser)
            .WithMany()
            .HasForeignKey(i => i.AssignedToUserId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Entity<IncidentUpdate>()
            .HasOne(u => u.Incident)
            .WithMany(i => i.Updates)
            .HasForeignKey(u => u.IncidentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<IncidentUpdate>()
            .HasOne(u => u.UpdatedByUser)
            .WithMany()
            .HasForeignKey(u => u.UpdatedByUserId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Entity<Vulnerability>()
            .HasOne(v => v.AffectedAsset)
            .WithMany()
            .HasForeignKey(v => v.AffectedAssetId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Entity<Vulnerability>()
            .HasOne(v => v.AssignedToUser)
            .WithMany()
            .HasForeignKey(v => v.AssignedToUserId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Entity<VulnerabilityUpdate>()
            .HasOne(u => u.Vulnerability)
            .WithMany(v => v.Updates)
            .HasForeignKey(u => u.VulnerabilityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<VulnerabilityUpdate>()
            .HasOne(u => u.UpdatedByUser)
            .WithMany()
            .HasForeignKey(u => u.UpdatedByUserId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Entity<EvidenceFile>()
            .HasOne(e => e.Incident)
            .WithMany()
            .HasForeignKey(e => e.IncidentId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Entity<EvidenceFile>()
            .HasOne(e => e.Vulnerability)
            .WithMany()
            .HasForeignKey(e => e.VulnerabilityId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Entity<EvidenceFile>()
            .HasOne(e => e.UploadedByUser)
            .WithMany()
            .HasForeignKey(e => e.UploadedByUserId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Entity<AuditLog>()
            .HasOne(a => a.User)
            .WithMany()
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
