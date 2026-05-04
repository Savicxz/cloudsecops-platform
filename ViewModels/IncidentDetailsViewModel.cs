using CloudSecOps.Models;

namespace CloudSecOps.ViewModels
{
    public class IncidentDetailsViewModel
    {
        public Incident Incident { get; set; } = new Incident();

        public List<InvestigationNote> InvestigationNotes { get; set; } = new List<InvestigationNote>();

        public InvestigationNote NewNote { get; set; } = new InvestigationNote();
    }
}