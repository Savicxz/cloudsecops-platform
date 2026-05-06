using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudSecOps.Web.Controllers;

[Authorize(Roles = "Manager,Administrator")]
public class ManagerController : Controller
{
    public IActionResult Index() => View();

    public IActionResult AssignAnalyst() => View();

    public IActionResult IncidentProgress() => View();
}
