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
    public async Task<ActionResult<IEnumerable<Votes>>> GetAllVotes()
    {
        var votes = await _voteService.GetAllVotes();
        return Ok(votes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Votes?>> GetVoteById(int id)
    {
        var votes = await _voteService.GetVoteById(id);
        return Ok(votes);
    }
}