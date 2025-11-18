using ClassLibrary;

namespace WebAPI.Interfaces.VoteInterfaces;

public interface IVoteService
{
    Task<IEnumerable<Votes>> GetAllVotes();
    Task<Votes?> GetVoteById(int id);
    Task<Votes> CreateVote(Votes vote);
    Task<Votes?> DeleteVote(int id);
}