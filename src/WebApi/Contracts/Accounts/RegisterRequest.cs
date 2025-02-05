using System.ComponentModel.DataAnnotations;

namespace WebApi.Contracts.Accounts;

public class RegisterRequest
{
    [EmailAddress]
    [Required(ErrorMessage = "Email is required.")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Password is required.")]
    public string Password { get; set; } = null!;

    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; } = null!;

    public string ClientUri { get; set; } = null!;
}
