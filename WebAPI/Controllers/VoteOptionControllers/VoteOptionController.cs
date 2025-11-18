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
    public async Task<ActionResult<IEnumerable<VoteOptions>>> GetAll()
    {
        var voteOptions = await _voteOptionService.GetAllVoteOptions();
        return Ok(voteOptions);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<VoteOptions?>> GetById(int id)
    {
        var voteOptions = await _voteOptionService.GetVoteOptionById(id);
        if (voteOptions == null)
        {
            return NotFound($"VoteOption with Id {id} not found");
        }
        return Ok(voteOptions);
    }

    [HttpPost]
    public async Task<ActionResult<VoteOptions>> Create(VoteOptions voteOption)
    {
        try
        {
            var createdVoteOption = await _voteOptionService.CreateVoteOption(voteOption);
            return CreatedAtAction(nameof(GetById), new {id = createdVoteOption.VoteOptionId},  createdVoteOption);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var deletedVoteOption = await _voteOptionService.DeleteVoteOption(id);
        if (deletedVoteOption == null)
        {
            return NotFound($"VoteOption with Id {id} not found");
        }
        return Ok(deletedVoteOption);
    }
}