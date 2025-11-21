using ClassLibrary;
using ClassLibrary.DTOs;

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
        return await _httpClient.GetFromJsonAsync<IEnumerable<Polls>>("api/poll");
    }

    public async Task<Polls?> GetPollById(int pollId)
    {
        return await _httpClient.GetFromJsonAsync<Polls?>($"api/poll/{pollId}");
    }

    public async Task CreatePoll(PollDTO poll)
    {
        await _httpClient.PostAsJsonAsync("api/poll", poll);
    }
    
    public async Task DeletePoll(int id)
    {
        await _httpClient.DeleteAsync($"api/poll/{id}");
    }
}