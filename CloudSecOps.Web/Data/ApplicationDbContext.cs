using CloudSecOps.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace CloudSecOps.Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<IncidentReport> IncidentReports { get; set; }
    }
}