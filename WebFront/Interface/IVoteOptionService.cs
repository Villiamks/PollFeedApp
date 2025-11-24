using ClassLibrary;

namespace WebFront.Services;

public interface IVoteOptionService
{
    Task<IEnumerable<VoteOptions>?> GetAllVoteOptions();
    Task<VoteOptions?> GetVoteOptionById(int id);
    Task<List<Votes>> GetVotes(VoteOptions voteOptions);
    Task CreateVoteOption(VoteOptions voteOptions);
    Task DeleteVoteOption(int id);
}