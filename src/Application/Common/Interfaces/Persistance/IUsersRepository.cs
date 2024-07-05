using Domain.Entities;

namespace Application.Common.Interfaces.Persistance;

public interface IUsersRepository
{
    public void Add(User user);
    public User? GetUserByEmail(string email);
}
