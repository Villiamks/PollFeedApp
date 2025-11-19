using ClassLibrary;

namespace WebFront.Services;

public interface IPollService
{
    Task<IEnumerable<Polls>?> GetAllPolls();
    Task<Polls?> GetPollById(int pollId);
    Task CreatePoll(Polls poll);
    Task DeletePoll(int id);
}