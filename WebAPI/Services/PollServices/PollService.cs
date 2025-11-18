using ClassLibrary;
using WebAPI.Interfaces;
using WebAPI.Interfaces.PollInterfaces;

namespace WebAPI.Services.PollServices;

public class PollService : IPollsService
{
    private IRepository<Polls> _pollRepository;

    public PollService(IRepository<Polls> pollRepository)
    {
        _pollRepository = pollRepository;
    }

    public async Task<IEnumerable<Polls>> GetAllPolls()
    {
        var  polls = await _pollRepository.GetAllAsync();
        return polls;
    }

    public async Task<Polls?> GetPollById(int id)
    {
        var  poll = await _pollRepository.GetByIdAsync(id);
        return poll;
    }

    public async Task<Polls> CreatePoll(Polls poll)
    {
        if (poll.Options == null || poll.Options.Count < 2)
        {
            throw new ArgumentException("A poll must have at least 2 options.");
        }
        return await _pollRepository.CreateAsync(poll);
    }

    public async Task<Polls?> DeletePoll(int id)
    {
        return await _pollRepository.DeleteAsync(id);
    }
}