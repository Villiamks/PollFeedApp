using ClassLibrary;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Interfaces.VoteInterfaces;

namespace WebAPI.Controllers.VoteControllers;

[ApiController]
[Route("api/[controller]")]
public class VoteController : ControllerBase
{
    private IVoteService _voteService;

    public VoteController(IVoteService voteService)
    {
        _voteService = voteService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Votes>>> GetAll()
    {
        var votes = await _voteService.GetAllVotes();
        return Ok(votes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Votes?>> GetById(int id)
    {
        var votes = await _voteService.GetVoteById(id);
        if (votes == null)
        {
            return NotFound($"Vote with id {id} not found");
        }
        return Ok(votes);
    }

    [HttpPost]
    public async Task<ActionResult<Votes>> Create(Votes vote)
    {
        try
        {
            var createdVote = await _voteService.CreateVote(vote);
            return CreatedAtAction(nameof(GetById), new {id = createdVote.VoteId}, createdVote);
        }
        catch (Exception e)
        {
           return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Votes>> Delete(int id)
    {
        var deletedVote = await _voteService.DeleteVote(id);
        if (deletedVote == null)
        {
            return NotFound($"Vote with id {id} not found");
        }
        return Ok(deletedVote);
    }
}