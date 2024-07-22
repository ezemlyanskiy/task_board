using System.ComponentModel.DataAnnotations;

namespace WebApi.Contracts.Accounts;

public class ForgotPasswordRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    public string ClientUri { get; set; } = null!;
}
