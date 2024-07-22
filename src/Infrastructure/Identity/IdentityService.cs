using Application.Authentication.Common;
using Application.Common.Interfaces.Services;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

public class IdentityService(UserManager<TaskBoardUser> userManager) : IIdentityService
{
    private readonly UserManager<TaskBoardUser> _userManager = userManager;

    public async Task<(IdentityResult, string)> Add(string userName, string password)
    {
        var user = new TaskBoardUser
        { 
            UserName = userName,
            Email = userName
        };

        var createResult = await _userManager.CreateAsync(user, password);

        return (createResult, user.Id);
    }

    public async Task<bool> DoesUserExistByEmail(string email)
    {
        return await _userManager.FindByEmailAsync(email) != null;
    }

    public async Task<bool> DoesUserExistById(string id)
    {
        return await _userManager.FindByIdAsync(id) != null;
    }

    public async Task AddToRoleAsync(string email, string role)
    {
        var user = await _userManager.FindByEmailAsync(email);

        await _userManager.AddToRoleAsync(user!, role);
    }

    public async Task<bool> IsLockedOutAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        return await _userManager.IsLockedOutAsync(user!);
    }

    public async Task<bool> CheckPasswordAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);

        return await _userManager.CheckPasswordAsync(user!, password);
    }

    public async Task<bool> IsEmailConfirmedAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        return await _userManager.IsEmailConfirmedAsync(user!);
    }

    public async Task<IList<string>?> GetRolesAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        return await _userManager.GetRolesAsync(user!);
    }

    public async Task<IdentityResult> AccessFailedAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        return await _userManager.AccessFailedAsync(user!);
    }

    public async Task<IdentityResult> ResetAccessFailedCountAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        return await _userManager.ResetAccessFailedCountAsync(user!);
    }

    public async Task<string> GenerateEmailConfirmationTokenAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        return await _userManager.GenerateEmailConfirmationTokenAsync(user!);
    }

    public async Task<UserDto> GetUserData(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        return new UserDto (user!.Id, user!.Email!);
    }

    public async Task<IdentityResult> ConfirmEmailAsync(string email, string token)
    {
        var user = await _userManager.FindByEmailAsync(email);

        return await _userManager.ConfirmEmailAsync(user!, token);
    }

    public async Task<string> GeneratePasswordResetTokenAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        var token = await _userManager.GeneratePasswordResetTokenAsync(user!);

        return token!;
    }

    public async Task<IdentityResult> ResetPasswordAsync(string email, string token, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);

        return await _userManager.ResetPasswordAsync(user!, token, password);
    }

    public async Task<IdentityResult> SetLockoutEndDateAsync(string email, bool block)
    {
        var user = await _userManager.FindByEmailAsync(email);

        return await _userManager.SetLockoutEndDateAsync(user!, block ? DateTimeOffset.MaxValue : DateTimeOffset.UtcNow);
    }
}
