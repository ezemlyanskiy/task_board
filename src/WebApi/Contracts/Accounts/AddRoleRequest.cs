using System.ComponentModel.DataAnnotations;

namespace WebApi.Contracts.Accounts;

public class AddRoleRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
    public string Role { get; set; } = null!;
}
