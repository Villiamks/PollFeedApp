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

    public async Task<Polls> GetPollById(int id)
    {
        var  poll = await _pollRepository.GetByIdAsync(id);
        return poll;
    }
}