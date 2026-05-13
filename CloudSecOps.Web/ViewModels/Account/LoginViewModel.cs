using System.ComponentModel.DataAnnotations;

namespace CloudSecOps.Web.ViewModels.Account;

public class LoginViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = "admin@cloudsecops.local";

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    public string? ReturnUrl { get; set; }
}
