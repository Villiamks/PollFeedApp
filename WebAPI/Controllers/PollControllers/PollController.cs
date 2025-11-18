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
    public async Task<ActionResult<IEnumerable<Polls>>> GetAll()
    {
        var polls = await _pollService.GetAllPolls();
        return Ok(polls);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Polls?>> GetById(int id)
    {
        var poll = await _pollService.GetPollById(id);
        if (poll == null)
        {
            return NotFound($"Poll with id {id} not found");
        }
        return Ok(poll);
    }

    [HttpPost]
    public async Task<ActionResult<Polls>> Create(Polls poll)
    {
        try
        {
            var createdPoll = await _pollService.CreatePoll(poll);
            return CreatedAtAction(nameof(GetById), new {id = createdPoll.PollId},  createdPoll);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var deletedPoll = await _pollService.DeletePoll(id);
        if (deletedPoll == null)
        {
            return NotFound($"Poll with id {id} not found");
        }
        return Ok(deletedPoll);
    }
}