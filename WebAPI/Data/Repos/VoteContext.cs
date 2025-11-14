using ClassLibrary;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Data.Repos;

public class VoteContext
{
    private DbContext _context;
    
    public List<Votes> GetVotes()
    {
        var votes = _context.Votes
            .Include(v => v.User)
            .Include(v => v.VoteOption)
            .ToList();
        return votes;
    }

    public async void AddVote(Votes vote)
    {
        _context.Votes.Add(vote);
        await _context.SaveChangesAsync();
    }
    
    public async void DeleteVote(Votes vote)
    {
        _context.Votes.Remove(vote);
        await _context.SaveChangesAsync();
    }
}