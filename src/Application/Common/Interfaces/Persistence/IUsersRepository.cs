namespace Application.Common.Interfaces.Persistence;

public interface IUsersRepository
{
    public void Add(User user);
    public User? GetUserByEmail(string email);
}
