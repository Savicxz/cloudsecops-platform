using CloudSecOps.Models;
using Microsoft.EntityFrameworkCore;

namespace CloudSecOps.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Incident> Incidents { get; set; }

        public DbSet<InvestigationNote> InvestigationNotes { get; set; }
    }
}