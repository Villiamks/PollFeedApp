using ClassLibrary;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Data.Repos;

public class PollContext
{
    private DbContext _context;
    
    public List<Polls> GetPolls()
    {
        var polls = _context.Polls
            .Include(p => p.Creator)
            .Include(p => p.Options)
            .ToList();
        return polls;
    }

    public async void AddPoll(Polls poll)
    {
        _context.Polls.Add(poll);
        await _context.SaveChangesAsync();
    }

    public async void DeletePoll(Polls poll)
    {
        _context.Polls.Remove(poll);
        await _context.SaveChangesAsync();
    }
}