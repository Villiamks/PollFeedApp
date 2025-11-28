using WebWasm.Models;
using WebWasm.DTOs;

namespace WebWasm.Services;

public interface IPollService
{
    Task<IEnumerable<Polls>?> GetAllPolls(IVoteOptionService voteOptionService);
    Task<Polls?> GetPollById(int pollId);
    Task CreatePoll(PollDTO poll);
    Task DeletePoll(int id);
}
