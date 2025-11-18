using ClassLibrary;
using WebAPI.Interfaces;
using WebAPI.Interfaces.VoteOptionsInterfaces;

namespace WebAPI.Services.VoteOptionServices;

public class VoteOptionService : IVoteOptionService
{
    
    private IRepository<VoteOptions> _voteOptionsRepository;

    public VoteOptionService(IRepository<VoteOptions> voteOptionsRepository)
    {
        _voteOptionsRepository = voteOptionsRepository;
    }

    public async Task<IEnumerable<VoteOptions>> GetAllVoteOptions()
    {
        var voteOptions = await _voteOptionsRepository.GetAllAsync();
        return voteOptions;
    }

    public async Task<VoteOptions?> GetVoteOptionById(int id)
    {
        var voteOption = await _voteOptionsRepository.GetByIdAsync(id);
        return voteOption;
    }

    public async Task<VoteOptions> CreateVoteOption(VoteOptions voteOption)
    {
        var votingOption = await _voteOptionsRepository.CreateAsync(voteOption);
        return votingOption;
    }

    public async Task<VoteOptions?> DeleteVoteOption(int id)
    {
        var votingOption = await _voteOptionsRepository.DeleteAsync(id);
        return votingOption;
    }
}