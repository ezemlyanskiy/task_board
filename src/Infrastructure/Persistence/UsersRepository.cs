using Application.Common.Interfaces.Persistance;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class UsersRepository(ApplicationDbContext context) : IUsersRepository
{
    private readonly ApplicationDbContext _context = context;

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
