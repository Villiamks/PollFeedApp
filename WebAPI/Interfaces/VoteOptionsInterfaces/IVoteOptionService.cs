using ClassLibrary;

namespace WebAPI.Interfaces.VoteOptionsInterfaces;

public interface IVoteOptionService
{
    Task<IEnumerable<VoteOptions>> GetAllVoteOptions();
    Task<VoteOptions?> GetVoteOptionById(int id);
    Task<VoteOptions> CreateVoteOption(VoteOptions voteOption);
    Task<VoteOptions?> DeleteVoteOption(int id);
}