using System.ComponentModel.DataAnnotations;

namespace WebApi.Contracts.Accounts;

public class UnblockUserRequest
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
}
