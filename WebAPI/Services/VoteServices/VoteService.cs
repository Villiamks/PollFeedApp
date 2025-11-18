using ClassLibrary;
using WebAPI.Interfaces;
using WebAPI.Interfaces.VoteInterfaces;

namespace WebAPI.Services.VoteServices;

public class VoteService : IVoteService
{
    
    private IRepository<Votes> _voteRepository;

    public VoteService(IRepository<Votes> voteRepository)
    {
        _voteRepository = voteRepository;
    }

    public async Task<IEnumerable<Votes>> GetAllVotes()
    {
        var votes = await _voteRepository.GetAllAsync();
        return votes;
    }

    public async Task<Votes> GetVoteById(int id)
    {
        var votes = await _voteRepository.GetByIdAsync(id);
        return votes;
    }
}