using System.ComponentModel.DataAnnotations;

namespace WebApi.Contracts.Accounts;

public class LoginRequest
{
    [EmailAddress]
    [Required(ErrorMessage = "Email is required.")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Password is required.")]
    public string Password { get; set; } = null!;
}
