using ClassLibrary;

namespace WebFront.Services;

public class PollService : IPollService
{
    private readonly HttpClient _httpClient;

    public PollService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Polls>?> GetAllPolls()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<Polls>>("api/polls");
    }

    public async Task<Polls?> GetPollById(int pollId)
    {
        return await _httpClient.GetFromJsonAsync<Polls?>($"api/polls/{pollId}");
    }

    public async Task CreatePoll(Polls poll)
    {
        await _httpClient.PostAsJsonAsync("api/polls", poll);
    }
    
    public async Task DeletePoll(int id)
    {
        await _httpClient.DeleteAsync($"api/polls/{id}");
    }
}