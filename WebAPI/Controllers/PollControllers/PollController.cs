using ClassLibrary;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Interfaces.PollInterfaces;

namespace WebAPI.Controllers.PollControllers;

[ApiController]
[Route("api/[controller]")]
public class PollController : ControllerBase
{
    private IPollsService _pollService;

    public PollController(IPollsService pollService)
    {
        _pollService = pollService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Polls>?>> GetAllPolls()
    {
        var polls = await _pollService.GetAllPolls();
        return Ok(polls);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Polls?>> GetPollById(int id)
    {
        var user = await _pollService.GetPollById(id);
        return Ok(user);
    }
}