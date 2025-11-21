using ClassLibrary;
using ClassLibrary.DTOs;

namespace WebFront.Services;

public interface IPollService
{
    Task<IEnumerable<Polls>?> GetAllPolls();
    Task<Polls?> GetPollById(int pollId);
    Task CreatePoll(PollDTO poll);
    Task DeletePoll(int id);
}