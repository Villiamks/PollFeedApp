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
            PollId =  poll.PollId,
            UserId = poll.UserId,
            Question = poll.Question,
            Options = poll.Options?.Select(opt => new VoteOptionDTO()
            {
                VoteOptionId =  opt.VoteOptionId,
                Caption = opt.Caption,
                Votes = opt.Votes?.Select(vote => new VoteDTO()
                {
                    VoteId = vote.VoteId,
                    VoteOptionId = vote.VoteOptionId,
                    UserId = vote.UserId
                }).ToList() ?? []
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
    public async Task<ActionResult<PollDTO>> Create(PollDTO dto)
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

            // Convert to DTO to avoid circular references
            var pollDto = new PollDTO()
            {
                PollId = createdPoll.PollId,
                UserId = createdPoll.UserId,
                Question = createdPoll.Question,
                Options = createdPoll.Options?.Select(opt => new VoteOptionDTO()
                {
                    VoteOptionId = opt.VoteOptionId,
                    Caption = opt.Caption,
                    Votes = []
                }).ToList() ?? []
            };

            return CreatedAtAction(nameof(GetById), new {id = createdPoll.PollId}, pollDto);
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