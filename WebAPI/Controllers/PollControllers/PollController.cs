using ClassLibrary;
using ClassLibrary.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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
    public async Task<ActionResult<IEnumerable<PollDTO>>> GetAll()
    {
        var polls = await _pollService.GetAllPolls();

        List<PollDTO> pollDtos = polls.Select(poll => new PollDTO()
        {
            UserId = poll.UserId,
            Question = poll.Question,
            Options = poll.Options?.Select(opt => new VoteOptionDTO()
            {
                Caption = opt.Caption
            }).ToList()
        }).ToList();
        
        return Ok(pollDtos);
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
    public async Task<ActionResult<Polls>> Create(PollDTO dto)
    {
        try
        {
            Polls poll = new Polls()
            {
                UserId =  dto.UserId,
                Question = dto.Question
            };
            poll.Options = dto.Options.Select(opt => new VoteOptions()
            {
                Caption =  opt.Caption,
            }).ToList();
            
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