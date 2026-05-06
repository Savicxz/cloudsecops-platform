using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudSecOps.Web.Controllers;

[Authorize(Roles = "SecurityAnalyst,Administrator")]
public class AnalystController : Controller
{
    public IActionResult Index() => View();
}
