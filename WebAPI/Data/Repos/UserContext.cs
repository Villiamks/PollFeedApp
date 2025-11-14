using ClassLibrary;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Data.Repos;

public class UserContext
{
    private DbContext _context;

    public List<Users> GetUsers()
    {
        var users  = _context.Users
            .Include(u => u.Polls)
            .Include(u => u.Votes)
            .ToList();
        return users;
    }

    public async Task<Users?> GetUserById(int id)
    {
        return GetUsers().FirstOrDefault(u => u.UserId == id);
    }
    
    public async void AddUser(Users newUser)
    {
        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();
    }

    public async void DeleteUser(Users newUser)
    {
        _context.Users.Remove(newUser);
        await _context.SaveChangesAsync();
    }

}