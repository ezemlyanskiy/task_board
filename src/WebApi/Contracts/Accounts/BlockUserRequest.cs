using System.ComponentModel.DataAnnotations;

namespace WebApi.Contracts.Accounts;

public class BlockUserRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
}
