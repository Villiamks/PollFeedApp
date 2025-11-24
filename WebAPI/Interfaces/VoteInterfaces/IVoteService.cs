using ClassLibrary;
using ClassLibrary.DTOs;

namespace WebAPI.Interfaces.VoteInterfaces;

public interface IVoteService
{
    Task<IEnumerable<VoteDTO>> GetAllVotes();
    Task<Votes?> GetVoteById(int id);
    Task<Votes> CreateVote(Votes vote);
    Task<Votes?> DeleteVote(int id);
}