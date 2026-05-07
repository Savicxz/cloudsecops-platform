using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudSecOps.Web.Controllers;

[Authorize(Roles = "Reporter,Administrator")]
public class ReporterController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}