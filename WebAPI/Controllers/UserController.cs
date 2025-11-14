using ClassLibrary;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data.Repos;
using WebAPI.Services;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private UserService _userService;
    
    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IEnumerable<Users>> GetALlUsers()
    {
        return await _userService.GetAllUsers();
    }

    [HttpGet("{id}")]
    public async Task<Users?> GetUser(int id)
    {
        return await _userService.GetUserById(id);
    }

    [HttpPost]
    public async Task CreateUser([FromBody] Users user)
    {
        await _userService.CreateUser(user);
    }

    [HttpDelete("{id}")]
    public async Task DeleteUser(int id)
    {
        await _userService.DeleteUser(id);
    }
}