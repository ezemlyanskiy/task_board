using System.ComponentModel.DataAnnotations;

namespace WebApi.Contracts.Accounts;

public class ResetPasswordRequest
{
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "The password and confirmation password do not match")]
    public string ConfirmPassword { get; set; } = null!;

    [EmailAddress]
    public string Email { get; set; } = null!;
    public string Token { get; set; } = null!;
}
