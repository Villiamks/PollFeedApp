using ClassLibrary;

namespace WebAPI.Interfaces.VoteOptionsInterfaces;

public interface IVoteOptionService
{
    Task<IEnumerable<VoteOptions>> GetAllVoteOptions();
    Task<VoteOptions> GetVoteOptionById(int id);
}