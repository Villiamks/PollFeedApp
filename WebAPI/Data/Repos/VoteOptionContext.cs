using ClassLibrary;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Data.Repos;

public class VoteOptionContext
{
    private DbContext _context;
    
    public List<VoteOptions> GetVoteOptions()
    {
        var voteOptions = _context.Options
            .Include(o => o.Poll)
            .Include(o => o.Votes)
            .ToList();
        return voteOptions;
    }

    public async void AddVoteOption(VoteOptions voteOption)
    {
        _context.Options.Add(voteOption);
        await _context.SaveChangesAsync();
    }

    public async void DeleteVoteOption(VoteOptions voteOption)
    {
        _context.Options.Remove(voteOption);
        await _context.SaveChangesAsync();
    }
}