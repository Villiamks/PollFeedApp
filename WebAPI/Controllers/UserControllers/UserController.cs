using ClassLibrary;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Interfaces;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Users>>> GetALlUsers()
    {
        var users = await _userService.GetAllUsers();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Users?>> GetUser(int id)
    {
        var user = await _userService.GetUserById(id);
        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    //TODO Post and Delete
}