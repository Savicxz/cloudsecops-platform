using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudSecOps.Web.Controllers;

[Authorize(Roles = "Auditor,Administrator")]
public class AuditController : Controller
{
    public IActionResult Index() => View();

    public IActionResult CompletedIncidents() => View();

    public IActionResult EvidenceReview() => View();
}
