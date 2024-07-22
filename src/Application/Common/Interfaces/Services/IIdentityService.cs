using Application.Authentication.Common;
using Microsoft.AspNetCore.Identity;

namespace Application.Common.Interfaces.Services;

public interface IIdentityService
{
    public Task<(IdentityResult, string)> Add(string userName, string password);
    public Task<bool> DoesUserExistByEmail(string email);
    public Task<bool> DoesUserExistById(string id);
    public Task AddToRoleAsync(string email, string role);
    public Task<IdentityResult> SetLockoutEndDateAsync(string email, bool block);
    public Task<bool> IsLockedOutAsync(string email);
    public Task<bool> CheckPasswordAsync(string email, string password);
    public Task<bool> IsEmailConfirmedAsync(string email);
    public Task<IList<string>?> GetRolesAsync(string email);
    public Task<IdentityResult> AccessFailedAsync(string email);
    public Task<IdentityResult> ResetAccessFailedCountAsync(string email);
    public Task<string> GenerateEmailConfirmationTokenAsync(string email);
    public Task<UserDto> GetUserData(string email);
    public Task<IdentityResult> ConfirmEmailAsync(string email, string token);
    public Task<string> GeneratePasswordResetTokenAsync(string email);
    public Task<IdentityResult> ResetPasswordAsync(string email, string token, string password);
}
