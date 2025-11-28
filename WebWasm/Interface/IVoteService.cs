using WebWasm.Models;

namespace WebWasm.Services;

public interface IVoteService
{
    Task<IEnumerable<Votes>?> GetVotes();
    Task<Votes?> GetVoteOption(int id);
    Task CreateVote(Votes vote);
    Task DeleteVote(int id);
}
