using ClassLibrary;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private Context.Context _context;
    
    public UserController(Context.Context context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IEnumerable<Users>> GetALlUsers()
    {
        return _context.getUsers();
    }

    [HttpGet("{id}")]
    public async Task<Users?> GetUser(int id)
    {
        return _context.getUsers().FirstOrDefault(u => u.UserId == id);
    }

    [HttpPost]
    public async Task CreateUser([FromBody] Users user)
    {
        Users newUser = new Users()
        {
            UserName = user.UserName,
            Email = user.Email,
            PasswordHash = user.PasswordHash,
            Salt = user.Salt,
        };
        
        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();
    }

    [HttpDelete("{id}")]
    public async Task DeleteUser(int id)
    {
        Users? user = _context.Users.FirstOrDefault(u => u.UserId == id);
        if (user == null)
        {
            return;
        }
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
}