using Domain.User.Enums;
using Domain.User.ValueObjects;

namespace Domain.User;

public class User : AggregateRoot<UserId>
{
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string Password { get; private set; } = null!;
    public Role Role { get; private set; } = Role.User;

    private User(
        UserId userId,
        string firstName,
        string lastName,
        string email,
        string password,
        Role role) : base(userId)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        Role = role;
    }

    public static User Create(
        string firstName,
        string lastName,
        string email,
        string password,
        Role role)
    {
        return new(
            UserId.CreateUnique(),
            firstName,
            lastName,
            email,
            password,
            role);
    }

#pragma warning disable CS8618
    private User()
    {
    }
#pragma warning restore CS8618
}
