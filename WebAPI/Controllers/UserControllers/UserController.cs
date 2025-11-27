using ClassLibrary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Interfaces;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController : ControllerBase
{
    private IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Users>>> GetAll()
    {
        var users = await _userService.GetAllUsers();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Users?>> GetById(int id)
    {
        var user = await _userService.GetUserById(id);
        if (user == null)
        {
            return  NotFound($"User with id {id} not found");
        }
        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<Users>> Create(Users user)
    {
        try
        {
            var createdUser = await _userService.CreateUser(user);
            return CreatedAtAction(nameof(GetById), new {id = createdUser.Id}, createdUser);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Users>> Delete(int id)
    {
        var deletedUser = await _userService.DeleteUserById(id);
        if (deletedUser == null)
        {
            return NotFound($"User with id {id} not found");
        }
        return Ok(deletedUser);
    }
}