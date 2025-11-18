using ClassLibrary;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Interfaces.VoteOptionsInterfaces;

namespace WebAPI.Controllers.VoteOptionControllers;

[ApiController]
[Route("api/[controller]")]
public class VoteOptionController : ControllerBase
{
    private IVoteOptionService _voteOptionService;

    public VoteOptionController(IVoteOptionService voteOptionService)
    {
        _voteOptionService = voteOptionService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<VoteOptions>>> GetAllVoteOptions()
    {
        var voteOptions = await _voteOptionService.GetAllVoteOptions();
        return Ok(voteOptions);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<VoteOptions?>> GetVoteOptionById(int id)
    {
        var voteOptions = await _voteOptionService.GetVoteOptionById(id);
        return Ok(voteOptions);
    }
}