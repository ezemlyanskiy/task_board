using Application.Common.Interfaces.Persistence;

namespace Infrastructure.Persistence.Repositories;

public class UsersRepository(TaskBoardDbContext context) : IUsersRepository
{
    private readonly TaskBoardDbContext _context = context;

    public void Add(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public User? GetUserByEmail(string email)
    {
        return _context.Users
            .AsNoTracking()
            .SingleOrDefault(u => u.Email == email);
    }
}
