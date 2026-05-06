using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudSecOps.Web.Controllers;

[Authorize(Roles = "Reporter")]
public class ReporterController : Controller
{
    public IActionResult Index() => View();

    public IActionResult SubmitReport() => View();

    public IActionResult MyReports() => View();

    public IActionResult Details() => View();
}
