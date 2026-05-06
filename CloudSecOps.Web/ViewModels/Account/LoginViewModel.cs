using System.ComponentModel.DataAnnotations;

namespace CloudSecOps.Web.ViewModels.Account;

public class LoginViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = "admin@cloudsecops.local";

    [Required]
    public string Role { get; set; } = "Administrator";

    public string? ReturnUrl { get; set; }
}
