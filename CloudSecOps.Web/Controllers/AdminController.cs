using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CloudSecOps.Web.Controllers;

[Authorize(Roles = "Administrator")]
public class AdminController : Controller
{
    public IActionResult Index() => View();
}
