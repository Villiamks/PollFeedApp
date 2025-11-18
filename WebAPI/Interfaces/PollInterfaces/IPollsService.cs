using ClassLibrary;

namespace WebAPI.Interfaces.PollInterfaces;

public interface IPollsService
{
    Task<IEnumerable<Polls>> GetAllPolls();
    Task<Polls?> GetPollById(int id);
    Task<Polls> CreatePoll(Polls poll);
    Task<Polls?> DeletePoll(int id);
    
}